using Code.Services;
using Code.StaticData;

namespace Code.UI.Services
{
  public interface IItemService : IService
  {
    void AddRandom(ItemType itemType, int quantity);
  }
}