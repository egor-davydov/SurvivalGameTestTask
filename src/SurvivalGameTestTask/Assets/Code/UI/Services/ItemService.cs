using System.Collections.Generic;
using Code.Data;
using Code.Services.Randomizer;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Services
{
  public class ItemService : IItemService
  {
    private readonly IInventoryService _inventoryService;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;

    private Inventory _inventory;

    public ItemService(IStaticDataService staticData, IInventoryService inventoryService, IRandomService randomService)
    {
      _randomService = randomService;
      _staticData = staticData;
      _inventoryService = inventoryService;
    }
    
    public void AddRandom(ItemType itemType, int quantity) =>
      PutInInventory(new ItemData(RandomItemOfCertainType(itemType).Id, quantity));

    public void AddStacksOf(ItemType itemType)
    {
      foreach (ItemStaticData itemStaticData in _staticData.ForItemsOfCertainType(itemType))
        PutInInventory(new ItemData(itemStaticData.Id, itemStaticData.MaxQuantityInStack));
    }

    public void ClearRandomSlot()
    {
      if (_inventoryService.OccupiedSlots.Count == 0)
      {
        Debug.LogError("No items in inventory");
        return;
      }

      int randomSlotNumber = _inventoryService.OccupiedSlots[_randomService.Next(0, _inventoryService.OccupiedSlots.Count)].Key;
      _inventoryService.RemoveItem(randomSlotNumber);
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
      return itemsOfType[_randomService.Next(0, itemsOfType.Count)];
    }  }
}