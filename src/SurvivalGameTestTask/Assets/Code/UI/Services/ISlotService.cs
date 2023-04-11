using Code.Services;
using Code.UI.InventoryWithSlots;

namespace Code.UI.Services
{
  public interface ISlotService : IService
  {
    bool TryToBuySlot(int siblingIndex);
    void Initialize(Inventory inventory);
  }
}