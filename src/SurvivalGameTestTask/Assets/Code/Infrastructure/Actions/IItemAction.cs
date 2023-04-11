using Code.UI.Services;

namespace Code.Infrastructure.Actions
{
  public interface IItemAction
  {
    void Construct(IItemService itemService);
  }
}