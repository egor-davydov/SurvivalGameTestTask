using Code.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.Actions
{
  public class RemoveItemButton : MonoBehaviour, IItemAction
  {
    [SerializeField]
    private Button Button;
  
    private IItemService _itemService;

    public void Construct(IItemService itemService) => 
      _itemService = itemService;

    private void Start() => 
      Button.onClick.AddListener(RemoveItem);

    private void RemoveItem() => 
      _itemService.ClearRandomSlot();
  }
}