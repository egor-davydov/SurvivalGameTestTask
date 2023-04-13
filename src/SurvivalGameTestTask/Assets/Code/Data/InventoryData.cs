using System;

namespace Code.Data
{
  [Serializable]
  public class InventoryData
  {
    public OccupiedSlotsDictionary OccupiedSlots = new();
    public int UnlockedSlotsQuantity;

    public event Action Changed;

    public void AddOccupiedSlot(int slotNumber, ItemData itemData)
    {
      ItemData occupiedSlotData = OccupiedSlots.Dictionary.TryGetValue(slotNumber, out ItemData data)
        ? data
        : null;

      if (occupiedSlotData == null)
        OccupiedSlots.Dictionary.Add(slotNumber, itemData);
      else
        occupiedSlotData.IncreaseQuantity(itemData.Quantity);
      
      Changed?.Invoke();
    }

    public void RemoveSlot(int slotNumber)
    {
      OccupiedSlots.Dictionary.Remove(slotNumber);
      Changed?.Invoke();
    }
  }
}