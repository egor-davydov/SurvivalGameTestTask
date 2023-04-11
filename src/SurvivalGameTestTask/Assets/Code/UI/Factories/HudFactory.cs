using Code.Infrastructure.Actions;
using Code.Infrastructure.AssetManagement;
using Code.Services.ProgressWatchers;
using Code.UI.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public class HudFactory : IHudFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IItemService _itemService;
    private readonly IProgressWatchersService _progressWatchers;

    public HudFactory(IAssetProvider assets, IItemService itemService, IProgressWatchersService progressWatchers)
    {
      _assets = assets;
      _itemService = itemService;
      _progressWatchers = progressWatchers;
    }
    
    public GameObject CreateHud()
    {
      GameObject hudPrefab = _assets.Load(AssetPath.HudPath);
      GameObject hudObject = Object.Instantiate(hudPrefab);
      _progressWatchers.Register(hudObject);
      foreach (IItemAction inventoryAction in hudObject.GetComponentsInChildren<IItemAction>())
        inventoryAction.Construct(_itemService);
      return hudObject;
    }
  }
}