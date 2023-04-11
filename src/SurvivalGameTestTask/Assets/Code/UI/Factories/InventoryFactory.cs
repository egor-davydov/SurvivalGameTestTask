using Code.Infrastructure.AssetManagement;
using Code.UI.InventoryWithSlots;
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

    public Inventory CreateInventory(Transform parent)
    {
      GameObject inventoryPrefab = _assets.Load(AssetPath.InventoryPath);
      GameObject inventoryObject = Object.Instantiate(inventoryPrefab, parent);
      Inventory inventory = inventoryObject.GetComponent<Inventory>();
      
      return inventory;
    }
  }
}