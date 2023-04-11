using UnityEngine;

namespace Code.UI.Factories
{
  public class HudFactory : IHudFactory
  {
    private const string HudPath = "Hud/Hud";

    public GameObject CreateHud()
    {
      GameObject hudPrefab = Resources.Load<GameObject>(HudPath);
      GameObject hudObject = Object.Instantiate(hudPrefab);
      
      return hudObject;
    }
  }
}