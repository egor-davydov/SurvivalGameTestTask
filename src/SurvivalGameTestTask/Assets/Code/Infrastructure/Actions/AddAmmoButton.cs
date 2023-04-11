using Code.StaticData;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.Actions
{
  public class AddAmmoButton : MonoBehaviour, IItemAction
  {
    [SerializeField]
    private Button Button;

    private IItemService _itemService;

    public void Construct(IItemService itemService) =>
      _itemService = itemService;

    private void Start() =>
      Button.onClick.AddListener(AddAmmo);

    private void AddAmmo()
    {
      _itemService.AddStacksOf(ItemType.Ammo);
    }
  }
}