using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.UI.Factories
{
  public class HudFactory : IHudFactory
  {
    private readonly IAssetProvider _assets;

    public HudFactory(IAssetProvider assets)
    {
      _assets = assets;
    }
    public GameObject CreateHud()
    {
      GameObject hudPrefab = _assets.Load(AssetPath.HudPath);
      GameObject hudObject = Object.Instantiate(hudPrefab);
      
      return hudObject;
    }
  }
}