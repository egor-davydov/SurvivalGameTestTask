using System.Collections.Generic;
using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    InventoryStaticData ForInventory();
    List<ItemStaticData> ForItemsOfCertainType(ItemType itemType);
    ItemStaticData ForItem(string id);
    LockedSlotStaticData ForLockedSlot();
  }
}