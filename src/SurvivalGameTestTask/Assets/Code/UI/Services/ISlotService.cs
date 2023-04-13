using Code.Services;

namespace Code.UI.Services
{
  public interface ISlotService : IService
  {
    bool TryToBuySlot(int siblingIndex);
  }
}