using System;

namespace Code.Data
{
  [Serializable]
  public class InventoryData
  {
    public OccupiedSlotsDictionary OccupiedSlots = new();
    public int UnlockedSlotsQuantity;

    public void AddOccupiedSlot(int slotNumber, ItemData itemData)
    {
      ItemData occupiedSlotData = OccupiedSlots.Dictionary.TryGetValue(slotNumber, out ItemData data)
        ? data
        : null;

      if (occupiedSlotData == null)
        OccupiedSlots.Dictionary.Add(slotNumber, itemData);
      else
        occupiedSlotData.ChangeData(itemData);
    }

    public void Remove(int slotNumber) =>
      OccupiedSlots.Dictionary.Remove(slotNumber);
  }
}