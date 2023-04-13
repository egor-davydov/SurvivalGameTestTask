using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.UI.Factories;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Services
{
  public class InventoryService : IInventoryService
  {
    private Inventory _inventory;

    private readonly IPersistentProgressService _progressService;
    private readonly ISlotFactory _slotFactory;

    private List<InventorySlot> Slots => _inventory.Slots;
    private List<KeyValuePair<int, ItemData>> OccupiedSlots => _inventory.OccupiedSlots;
    private Queue<LockedSlot> LockedSlots => _inventory.LockedSlots;

    private InventoryData InventoryProgressData => _progressService.Progress.InventoryData;

    public int OccupiedSlotsCount => OccupiedSlots.Count;

    public InventoryService(IPersistentProgressService progressService, ISlotFactory slotFactory)
    {
      _slotFactory = slotFactory;
      _progressService = progressService;
    }

    public void Initialize(Inventory inventory)
    {
      _inventory = inventory;
    }

    public void MakeNextSlotUnlockable() =>
      LockedSlots.Dequeue().MakeUnlockable();

    public void AddItem(ItemData itemData)
    {
      foreach (InventorySlot slot in Slots)
      {
        if (slot.IsEmpty)
        {
          InventoryProgressData.AddOccupiedSlot(slot.SlotNumber, itemData);
          return;
        }

        if (SameNotFullItem(slot, itemData))
        {
          if (SlotNotFitsAllQuantity(itemData, slot))
          {
            TradeQuantityForStack(itemData, slot);
            AddItem(itemData);
            return;
          }

          InventoryProgressData.AddOccupiedSlot(slot.SlotNumber, itemData);
          return;
        }
      }

      Debug.LogError("Cant AddItem. Inventory is full");
    }

    public void AddSlot(int siblingIndex)
    {
      InventorySlot inventorySlot = _slotFactory.CreateSlot(_inventory.SlotsParent);
      inventorySlot.transform.SetSiblingIndex(siblingIndex);
      Slots.Add(inventorySlot);
      inventorySlot.Initialize(Slots.Count - 1);
    }

    public void RemoveItem(int slotNumber) =>
      InventoryProgressData.RemoveSlot(slotNumber);

    public void DecreaseItemQuantity(ItemData itemData)
    {
      foreach (KeyValuePair<int, ItemData> occupiedSlot in OccupiedSlots) 
      {
        if (occupiedSlot.Value.Id != itemData.Id)
          continue;

        if (occupiedSlot.Value.Quantity <= itemData.Quantity)
        {
          InventoryProgressData.RemoveSlot(occupiedSlot.Key);
          if (occupiedSlot.Value.Quantity < itemData.Quantity)
          {
            itemData.DecreaseQuantity(occupiedSlot.Value.Quantity);
            DecreaseItemQuantity(itemData);
          }
        }
        else
          InventoryProgressData.DecreaseQuantity(occupiedSlot.Key, itemData.Quantity);

        return;
      }
    }

    private void TradeQuantityForStack(ItemData item, InventorySlot slot)
    {
      int quantityNeededForStack = slot.Capacity - slot.Quantity;
      InventoryProgressData.IncreaseQuantity(slot.SlotNumber, quantityNeededForStack);
      item.DecreaseQuantity(quantityNeededForStack);
    }

    private bool SlotNotFitsAllQuantity(ItemData item, InventorySlot slot) =>
      item.Quantity + slot.Quantity > slot.Capacity;

    private bool SameNotFullItem(InventorySlot slot, ItemData item) =>
      !slot.IsEmpty && SameType(slot, item) && !slot.IsFull;

    private bool SameType(InventorySlot slot, ItemData item) =>
      slot.ItemId == item.Id;

    public int QuantityOf(string id) =>
      OccupiedSlots.Where(x => x.Value.Id == id).Sum(x => x.Value.Quantity);
  }
}