using System.Collections.Generic;
using Code.Services;
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
    private readonly IStaticDataService _staticData;

    private Inventory _inventory;

    public ItemService(IStaticDataService staticData, IItemFactory itemFactory)
    {
      _staticData = staticData;
      _itemFactory = itemFactory;
    }

    public void Initialize(Inventory inventory)
    {
      _inventory = inventory;
    }

    public void AddRandom(ItemType itemType, int quantity)
    {
      InventoryItem inventoryItem = CreateItem();
      inventoryItem.Initialize(RandomItemOfCertainType(itemType), quantity);

      PutInInventory(inventoryItem);
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