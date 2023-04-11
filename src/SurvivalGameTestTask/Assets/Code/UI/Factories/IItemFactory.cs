using Code.Services;
using UnityEngine;

namespace Code.UI.Services
{
  public interface IItemFactory : IService
  {
    InventoryItem CreateItem(Transform parent);
  }
}