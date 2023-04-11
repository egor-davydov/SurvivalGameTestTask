using Code.UI.Services;

namespace Code.UI.InventoryWithSlots
{
  public class InventorySlot
  {
    public int SlotNumber { get; private set; }
    public string ItemId => InventoryItem.Id;
    public int Quantity => InventoryItem.Quantity;
    public int Capacity => InventoryItem.MaxQuantityInStack;
    public bool IsEmpty => InventoryItem == null;
    public bool IsFull => Capacity == Quantity;

    private InventoryItem InventoryItem { get; set; }

    public void AddItem(InventoryItem item)
    {
      
    }
    public void IncreaseQuantity(int itemQuantity) => 
      InventoryItem.IncreaseQuantity(itemQuantity);

    public void DecreaseQuantity(int itemQuantity) => 
      InventoryItem.DecreaseQuantity(itemQuantity);
  }
}