using Code.Infrastructure.AssetManagement;
using Code.Services.ProgressWatchers;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public class InventoryFactory : IInventoryFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IProgressWatchersService _progressWatchers;

    public InventoryFactory(IAssetProvider assets, IProgressWatchersService progressWatchers)
    {
      _assets = assets;
      _progressWatchers = progressWatchers;
    }

    public Inventory CreateInventory(Transform parent)
    {
      GameObject inventoryPrefab = _assets.Load(AssetPath.InventoryPath);
      GameObject inventoryObject = Object.Instantiate(inventoryPrefab, parent);
      _progressWatchers.Register(inventoryObject);
      Inventory inventory = inventoryObject.GetComponent<Inventory>();
      
      return inventory;
    }
  }
}