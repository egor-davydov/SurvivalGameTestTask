using Code.Services;
using Code.StaticData;
using Code.UI.InventoryWithSlots;

namespace Code.UI.Services
{
  public interface IItemService : IService
  {
    void AddRandom(ItemType itemType, int quantity);
    void Initialize(Inventory inventory);
  }
}