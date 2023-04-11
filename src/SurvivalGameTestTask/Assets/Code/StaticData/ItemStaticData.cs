using UnityEngine;

namespace Code.StaticData
{
  public class ItemStaticData : ScriptableObject
  {
    public string Id;
    public float Weight;
    public int MaxQuantityInStack;
    public Sprite Icon;
    public ItemType ItemType;
  }
}