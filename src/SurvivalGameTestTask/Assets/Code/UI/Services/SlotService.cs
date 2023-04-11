using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factories;
using Code.UI.InventoryWithSlots;

namespace Code.UI.Services
{
  public class SlotService : ISlotService
  {
    private readonly IStaticDataService _staticData;
    private readonly ISlotFactory _slotFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private Inventory _inventory;

    public SlotService(IStaticDataService staticData, ISlotFactory slotFactory, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
      _progressService = progressService;
      _slotFactory = slotFactory;
      _staticData = staticData;
    }

    public void Initialize(Inventory inventory)
    {
      _inventory = inventory;
    }

    public bool TryToBuySlot(int siblingIndex)
    {
      LockedSlotStaticData lockedSlotStaticData = _staticData.ForLockedSlot();
      if (_inventory.QuantityOf(lockedSlotStaticData.PriceItemId) < lockedSlotStaticData.Price)
        return false;

      _inventory.DecreaseItemQuantity(lockedSlotStaticData.PriceItemId, lockedSlotStaticData.Price);
      InventorySlot inventorySlot = _slotFactory.CreateSlot(_inventory.SlotsParent);
      inventorySlot.transform.SetSiblingIndex(siblingIndex);
      _inventory.AddSlot(inventorySlot);
      _inventory.MakeNextSlotUnlockable();
      _progressService.Progress.InventoryData.UnlockedSlotsQuantity++;
      _saveLoadService.SaveProgress();

      return true;
    }
  }
}