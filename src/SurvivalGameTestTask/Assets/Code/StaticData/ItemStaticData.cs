using UnityEngine;

namespace Code.StaticData
{
  public class ItemStaticData : ScriptableObject
  {
    public string Id;

    [Range(0.01f, 50)]
    public float Weight;

    [Range(1, 20)]
    public int MaxQuantityInStack;

    public Sprite Icon;
    public ItemType ItemType;
  }
}