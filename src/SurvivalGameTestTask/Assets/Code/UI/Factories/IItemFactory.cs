using Code.Services;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface IItemFactory : IService
  {
    InventoryItem CreateItem(Transform parent);
  }
}