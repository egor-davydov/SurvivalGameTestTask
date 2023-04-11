using Code.Infrastructure.AssetManagement;
using Code.Services.ProgressWatchers;
using Code.Services.StaticData;
using Code.UI.InventoryWithSlots;
using Code.UI.Services;
using UnityEngine;

namespace Code.UI.Factories
{
  public class SlotFactory : ISlotFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IProgressWatchersService _progressWatchers;
    private readonly IStaticDataService _staticData;
    private ISlotService _slotService;

    public SlotFactory(IAssetProvider assets, IProgressWatchersService progressWatchers, IStaticDataService staticData)
    {
      _assets = assets;
      _progressWatchers = progressWatchers;
      _staticData = staticData;
    }

    public void Initialize(ISlotService slotService)
    {
      _slotService = slotService;
    }

    public InventorySlot CreateSlot(Transform parent)
    {
      GameObject slotPrefab = _assets.Load(AssetPath.SlotPath);
      GameObject slotObject = Object.Instantiate(slotPrefab, parent);
      _progressWatchers.Register(slotObject);
      var inventorySlot = slotObject.GetComponent<InventorySlot>();
      return inventorySlot;
    }

    public LockedSlot CreateLockedSlot(Transform parent)
    {
      GameObject slotPrefab = _assets.Load(AssetPath.LockedSlotPath);
      GameObject slotObject = Object.Instantiate(slotPrefab, parent);
      _progressWatchers.Register(slotObject);
      LockedSlot lockedSlot = slotObject.GetComponent<LockedSlot>();
      lockedSlot.Construct(_slotService, _staticData);

      return lockedSlot;
    }
  }
}