using System.Collections.Generic;
using Code.UI.Services;
using UnityEngine;

namespace Code.UI.InventoryWithSlots
{
  public class Inventory : MonoBehaviour
  {
    [SerializeField]
    private Transform _slotsParent;

    private List<InventorySlot> _slots;

    public Transform SlotsParent
    {
      get => _slotsParent;
      set => _slotsParent = value;
    }

    public void Initialize(List<InventorySlot> slots)
    {
      _slots = slots;
    }
    
    public void AddItem(InventoryItem item)
    {
      foreach (InventorySlot slot in _slots)
      {
        if (slot.IsEmpty)
        {
          slot.AddItem(item);
          return;
        }

        if (SameNotFullItem(slot, item))
        {
          if (SlotNotFitsAllQuantity(item, slot))
          {
            TradeQuantityForStack(item, slot);

            AddItem(item);
            return;
          }

          Destroy(item.gameObject);
          slot.IncreaseQuantity(item.Quantity);
          return;
        }
      }

      Destroy(item.gameObject);
      Debug.LogError("Cant AddItem. Inventory is full");
    }

    private bool SameNotFullItem(InventorySlot slot, InventoryItem item) =>
      !slot.IsEmpty && SameType(slot, item) && !slot.IsFull;

    private bool SlotNotFitsAllQuantity(InventoryItem item, InventorySlot slot) =>
      item.Quantity + slot.Quantity > slot.Capacity;

    private void TradeQuantityForStack(InventoryItem item, InventorySlot slot)
    {
      int quantityNeededForStack = slot.Capacity - slot.Quantity;
      slot.IncreaseQuantity(quantityNeededForStack);
      item.DecreaseQuantity(quantityNeededForStack);
    }
    private bool SameType(InventorySlot slot, InventoryItem item) =>
      slot.ItemId == item.Id;
  }
}