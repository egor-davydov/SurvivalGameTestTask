using Code.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface IInventoryFactory : IService
  {
    void CreateInventory(Transform parent);
  }
}