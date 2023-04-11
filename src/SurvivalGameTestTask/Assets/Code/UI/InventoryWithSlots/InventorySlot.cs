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

    public void AddItem(InventoryItem inventoryItem)
    {
      InventoryItem = inventoryItem;
      InventoryItem.transform.SetParent(transform);
      InventoryItem.transform.position = transform.position;
    }

    public void RemoveItem()
    {
      Destroy(InventoryItem.gameObject);
      Destroy(InventoryItem);
    }

    public void IncreaseQuantity(int itemQuantity) =>
      InventoryItem.IncreaseQuantity(itemQuantity);

    public void DecreaseQuantity(int itemQuantity) =>
      InventoryItem.DecreaseQuantity(itemQuantity);

    public int QuantityOf(string id)
    {
      if (id == ItemId)
        return Quantity;

      return 0;
    }
  }
}