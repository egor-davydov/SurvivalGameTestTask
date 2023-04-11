using Code.Infrastructure.AssetManagement;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public class SlotFactory : ISlotFactory
  {
    private readonly IAssetProvider _assets;

    public SlotFactory(IAssetProvider assets)
    {
      _assets = assets;
    }
    public InventorySlot CreateSlot(Transform parent)
    {
      GameObject slotPrefab = _assets.Load(AssetPath.SlotPath);
      GameObject slotObject = Object.Instantiate(slotPrefab, parent);
      var inventorySlot = slotObject.GetComponent<InventorySlot>();
      return inventorySlot;
    }
  }
}