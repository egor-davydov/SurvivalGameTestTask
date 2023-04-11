using Code.Infrastructure.AssetManagement;
using Code.Services.ProgressWatchers;
using Code.Services.StaticData;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public class InventoryFactory : IInventoryFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IProgressWatchersService _progressWatchers;
    private readonly IStaticDataService _staticData;
    private readonly IItemFactory _itemFactory;

    public InventoryFactory(IAssetProvider assets, IProgressWatchersService progressWatchers, IStaticDataService staticData, IItemFactory itemFactory)
    {
      _assets = assets;
      _progressWatchers = progressWatchers;
      _staticData = staticData;
      _itemFactory = itemFactory;
    }

    public Inventory CreateInventory(Transform parent)
    {
      GameObject inventoryPrefab = _assets.Load(AssetPath.InventoryPath);
      GameObject inventoryObject = Object.Instantiate(inventoryPrefab, parent);
      _progressWatchers.Register(inventoryObject);
      Inventory inventory = inventoryObject.GetComponent<Inventory>();
      inventory.Construct(_staticData, _itemFactory);
      return inventory;
    }
  }
}