using Code.Infrastructure.Actions;
using Code.Infrastructure.AssetManagement;
using Code.UI.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public class HudFactory : IHudFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IItemService _itemService;

    public HudFactory(IAssetProvider assets, IItemService itemService)
    {
      _assets = assets;
      _itemService = itemService;
    }
    
    public GameObject CreateHud()
    {
      GameObject hudPrefab = _assets.Load(AssetPath.HudPath);
      GameObject hudObject = Object.Instantiate(hudPrefab);
      
      foreach (IItemAction inventoryAction in hudObject.GetComponentsInChildren<IItemAction>())
        inventoryAction.Construct(_itemService);
      return hudObject;
    }
  }
}