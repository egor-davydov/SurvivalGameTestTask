using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/Item/Outfit", fileName = "OutfitStaticData", order = 0)]
  class OutfitStaticData : ItemStaticData
  {
    public ProtectionType ProtectionType;

    [Range(1, 100)]
    public int Protection;
  }
}