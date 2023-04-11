using Code.Services.StaticData;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/Item/Weapon", fileName = "WeaponStaticData", order = 0)]
  public class WeaponStaticData : ItemStaticData
  {
    public WeaponType WeaponType;
    public BulletType ConsumableAmmo;
    public int Damage;
  }
}