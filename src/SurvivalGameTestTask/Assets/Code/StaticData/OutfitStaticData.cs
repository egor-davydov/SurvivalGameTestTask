using Code.Services.StaticData;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/Item/Outfit", fileName = "OutfitStaticData", order = 0)]
  class OutfitStaticData : ItemStaticData
  {
    public ProtectionType ProtectionType;
    public int Protection;
  }
}