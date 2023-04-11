using Code.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public interface IHudFactory : IService
  {
    GameObject CreateHud();
  }
}