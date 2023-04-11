using Code.UI.Services;
using UnityEngine;

namespace Code.UI.InventoryWithSlots
{
  public class Inventory : MonoBehaviour
  {
    [SerializeField]
    private Transform _slotsParent;

    public Transform SlotsParent
    {
      get => _slotsParent;
      set => _slotsParent = value;
    }

    public void AddItem(InventoryItem inventoryItem)
    {
      
    }
  }
}