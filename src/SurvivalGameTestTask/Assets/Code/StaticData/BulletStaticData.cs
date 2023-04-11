using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(menuName = "StaticData/Item/Bullet", fileName = "BulletStaticData", order = 0)]
  public class BulletStaticData : ItemStaticData
  {
    public BulletType BulletType;
  }
}