using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/Inventory", fileName = "InventoryStaticData", order = 0)]
  public class InventoryStaticData : ScriptableObject
  {
    public int SlotsQuantity;
    public int LockedSlotsQuantity;
  }
}