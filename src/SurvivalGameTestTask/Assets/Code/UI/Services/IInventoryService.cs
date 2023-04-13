using Code.Data;
using Code.Services;
using Code.UI.InventoryWithSlots;

namespace Code.UI.Services
{
  public interface IInventoryService : IService
  {
    void Initialize(Inventory inventory);
    void MakeNextSlotUnlockable();
    void AddItem(ItemData itemData);
    void AddSlot(int siblingIndex);
    void RemoveItem(int slotNumber);
    void DecreaseItemQuantity(ItemData itemData);
    int QuantityOf(string id);
    int OccupiedSlotsCount { get; }
  }
}