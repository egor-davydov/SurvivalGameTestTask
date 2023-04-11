using Code.StaticData;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.UI;
namespace Code.Infrastructure.Actions
{
  public class AddItemsButton : MonoBehaviour, IItemAction
  {
    [SerializeField]
    private Button Button;
  
    private IItemService _itemService;

    public void Construct(IItemService itemService) => 
      _itemService = itemService;

    private void Start() => 
      Button.onClick.AddListener(AddItems);

    private void AddItems()
    {
      _itemService.AddRandom(ItemType.Weapon, 1);
      _itemService.AddRandom(ItemType.HeadOutfit, 1);
      _itemService.AddRandom(ItemType.BodyOutfit, 1);
    }
  }
}