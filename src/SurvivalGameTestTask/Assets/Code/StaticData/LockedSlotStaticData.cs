using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/LockedSlot", fileName = "LockedSlotStaticData", order = 0)]
  public class LockedSlotStaticData : ScriptableObject
  {
    public string PriceItemId;

    [Range(1, 60)]
    public int Price;
  }
}