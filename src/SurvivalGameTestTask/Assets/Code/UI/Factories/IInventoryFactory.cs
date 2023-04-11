using Code.Services;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface IInventoryFactory : IService
  {
    Inventory CreateInventory(Transform parent);
  }
}