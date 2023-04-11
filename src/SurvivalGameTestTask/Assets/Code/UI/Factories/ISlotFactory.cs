using Code.Services;
using Code.UI.InventoryWithSlots;
using Code.UI.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface ISlotFactory : IService
  {
    InventorySlot CreateSlot(Transform parent);
    LockedSlot CreateLockedSlot(Transform slotsParent);
    void Initialize(ISlotService slotService);
  }
}