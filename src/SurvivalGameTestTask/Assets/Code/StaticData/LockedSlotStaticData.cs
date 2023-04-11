using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/LockedSlot", fileName = "LockedSlotStaticData", order = 0)]
  public class LockedSlotStaticData : ScriptableObject
  {
    public string PriceItemId;
    public int Price;
  }
}