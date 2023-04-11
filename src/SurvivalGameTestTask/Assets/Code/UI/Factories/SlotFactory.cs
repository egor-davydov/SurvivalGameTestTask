using Code.Infrastructure.AssetManagement;
using Code.Services.ProgressWatchers;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public class SlotFactory : ISlotFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IProgressWatchersService _progressWatchers;

    public SlotFactory(IAssetProvider assets, IProgressWatchersService progressWatchers)
    {
      _assets = assets;
      _progressWatchers = progressWatchers;
    }
    public InventorySlot CreateSlot(Transform parent)
    {
      GameObject slotPrefab = _assets.Load(AssetPath.SlotPath);
      GameObject slotObject = Object.Instantiate(slotPrefab, parent);
      _progressWatchers.Register(slotObject);
      var inventorySlot = slotObject.GetComponent<InventorySlot>();
      return inventorySlot;
    }
  }
}