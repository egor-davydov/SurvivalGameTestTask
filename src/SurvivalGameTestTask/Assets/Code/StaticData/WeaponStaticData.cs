using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/Item/Weapon", fileName = "WeaponStaticData", order = 0)]
  public class WeaponStaticData : ItemStaticData
  {
    public WeaponType WeaponType;
    public BulletType ConsumableAmmo;

    [Range(1, 100)]
    public int Damage;
  }
}