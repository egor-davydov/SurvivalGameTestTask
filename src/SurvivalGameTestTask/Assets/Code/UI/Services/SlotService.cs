using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.InventoryWithSlots;

namespace Code.UI.Services
{
  public class SlotService : ISlotService
  {
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IInventoryService _inventoryService;
    private Inventory _inventory;

    public SlotService(IStaticDataService staticData, IPersistentProgressService progressService, IInventoryService inventoryService)
    {
      _inventoryService = inventoryService;
      _progressService = progressService;
      _staticData = staticData;
    }

    public bool TryToBuySlot(int siblingIndex)
    {
      LockedSlotStaticData lockedSlotStaticData = _staticData.ForLockedSlot();
      if (_inventoryService.QuantityOf(lockedSlotStaticData.PriceItemId) < lockedSlotStaticData.Price)
        return false;

      _inventoryService.DecreaseItemQuantity(new ItemData(lockedSlotStaticData.PriceItemId, lockedSlotStaticData.Price));
      _inventoryService.AddSlot(siblingIndex);
      _inventoryService.MakeNextSlotUnlockable();
      _progressService.Progress.InventoryData.UnlockedSlotsQuantity++;

      return true;
    }
  }
}