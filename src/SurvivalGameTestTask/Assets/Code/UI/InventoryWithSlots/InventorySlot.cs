using UnityEngine;

namespace Code.UI.InventoryWithSlots
{
  public class InventorySlot : MonoBehaviour
  {
    public int SlotNumber { get; private set; }
    public string ItemId => InventoryItem.Id;
    public int Quantity => InventoryItem.Quantity;
    public int Capacity => InventoryItem.MaxQuantityInStack;
    public bool IsEmpty => InventoryItem == null;
    public bool IsFull => Capacity == Quantity;

    private InventoryItem InventoryItem { get; set; }

    public void Initialize(int slotNumber) =>
      SlotNumber = slotNumber;

    public void AddItem(InventoryItem inventoryItem) => 
      InventoryItem = inventoryItem;

    public void RemoveItem()
    {
      Destroy(InventoryItem.gameObject);
      Destroy(InventoryItem);
    }
  }
}