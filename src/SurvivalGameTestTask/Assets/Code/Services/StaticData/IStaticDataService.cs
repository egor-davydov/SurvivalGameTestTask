using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    InventoryStaticData ForInventory();
  }
}