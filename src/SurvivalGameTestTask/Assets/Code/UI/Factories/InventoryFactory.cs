using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.UI.Factories
{
  public class InventoryFactory : IInventoryFactory
  {
    private readonly IAssetProvider _assets;

    public InventoryFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public void CreateInventory(Transform parent)
    {
      GameObject inventoryPrefab = _assets.Load(AssetPath.InventoryPath);
      GameObject inventoryObject = Object.Instantiate(inventoryPrefab, parent);
    }
  }
}