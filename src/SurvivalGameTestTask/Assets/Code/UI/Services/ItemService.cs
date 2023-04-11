using System.Collections.Generic;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factories;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Services
{
  public class ItemService : IItemService
  {
    private readonly IItemFactory _itemFactory;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IStaticDataService _staticData;

    private Inventory _inventory;

    public ItemService(IStaticDataService staticData, IItemFactory itemFactory, ISaveLoadService saveLoadService)
    {
      _staticData = staticData;
      _itemFactory = itemFactory;
      _saveLoadService = saveLoadService;
    }

    public void Initialize(Inventory inventory)
    {
      _inventory = inventory;
    }

    public void AddStacksOf(ItemType itemType)
    {
      List<ItemStaticData> itemsOfCertainType = _staticData.ForItemsOfCertainType(itemType);
      foreach (ItemStaticData itemStaticData in itemsOfCertainType)
      {
        InventoryItem inventoryItem = CreateItem();
        inventoryItem.Initialize(itemStaticData, itemStaticData.MaxQuantityInStack);

        PutInInventory(inventoryItem);
      }
      _saveLoadService.SaveProgress();
    }

    public void ClearRandomSlot()
    {
      _inventory.ClearRandomSlot();
      _saveLoadService.SaveProgress();
    }

    public void DecreaseRandomItem(ItemType itemType, int quantity)
    {
      ItemStaticData randomItemOfCertainType = RandomItemOfCertainType(itemType);
      _inventory.DecreaseItemQuantity(randomItemOfCertainType.Id, quantity);
      _saveLoadService.SaveProgress();
    }

    public void AddRandom(ItemType itemType, int quantity)
    {
      InventoryItem inventoryItem = CreateItem();
      inventoryItem.Initialize(RandomItemOfCertainType(itemType), quantity);

      PutInInventory(inventoryItem);
      _saveLoadService.SaveProgress();
    }

    private InventoryItem CreateItem() =>
      _itemFactory.CreateItem(_inventory.SlotsParent);

    private void PutInInventory(InventoryItem inventoryItem) =>
      _inventory.AddItem(inventoryItem);

    private ItemStaticData RandomItemOfCertainType(ItemType itemType)
    {
      List<ItemStaticData> itemsOfType = _staticData.ForItemsOfCertainType(itemType);
      return itemsOfType[Random.Range(0, itemsOfType.Count)];
    }
  }
}