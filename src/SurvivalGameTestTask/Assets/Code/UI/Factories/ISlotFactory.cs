using Code.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface ISlotFactory : IService
  {
    void CreateSlot(Transform parent);
  }
}