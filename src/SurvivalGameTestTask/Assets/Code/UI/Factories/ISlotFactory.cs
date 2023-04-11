using Code.Services;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface ISlotFactory : IService
  {
    InventorySlot CreateSlot(Transform parent);
  }
}