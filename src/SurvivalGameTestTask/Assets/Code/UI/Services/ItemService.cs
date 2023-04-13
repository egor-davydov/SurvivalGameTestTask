using System.Collections.Generic;
using Code.Data;
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
    private readonly IInventoryService _inventoryService;
    private readonly IStaticDataService _staticData;

    private Inventory _inventory;

    public ItemService(IStaticDataService staticData, IItemFactory itemFactory, ISaveLoadService saveLoadService, IInventoryService inventoryService)
    {
      _staticData = staticData;
      _itemFactory = itemFactory;
      _saveLoadService = saveLoadService;
      _inventoryService = inventoryService;
    }
    
    public void AddRandom(ItemType itemType, int quantity) =>
      PutInInventory(new ItemData(RandomItemOfCertainType(itemType).Id, quantity));

    public void AddStacksOf(ItemType itemType)
    {
      ItemStaticData randomItemData = RandomItemOfCertainType(itemType);
      PutInInventory(new ItemData(randomItemData.Id, randomItemData.MaxQuantityInStack));
    }

    public void ClearRandomSlot()
    {
      int occupiedSlotsCount = _inventoryService.OccupiedSlotsCount;
      if (occupiedSlotsCount == 0)
      {
        Debug.LogError("No items in inventory");
        return;
      }

      _inventoryService.RemoveItem(Random.Range(0, occupiedSlotsCount));
    }

    public void DecreaseRandomItem(ItemType itemType, int quantity)
    {
      ItemStaticData randomItemOfCertainType = RandomItemOfCertainType(itemType);
      _inventoryService.DecreaseItemQuantity(new ItemData(randomItemOfCertainType.Id, quantity));
    }

    private void PutInInventory(ItemData inventoryItem) =>
      _inventoryService.AddItem(inventoryItem);

    private ItemStaticData RandomItemOfCertainType(ItemType itemType)
    {
      List<ItemStaticData> itemsOfType = _staticData.ForItemsOfCertainType(itemType);
      return itemsOfType[Random.Range(0, itemsOfType.Count)];
    }  }
}