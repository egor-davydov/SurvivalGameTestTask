using Code.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.InventoryWithSlots
{
  public class InventoryItem : MonoBehaviour
  {
    [SerializeField]
    private Image Icon;

    [SerializeField]
    private TextMeshProUGUI QuantityText;

    private int _quantity;

    public string Id { get; private set; }
    public float Weight { get; private set; }
    public int MaxQuantityInStack { get; private set; }

    public int Quantity
    {
      get => _quantity;
      private set
      {
        _quantity = value;
        QuantityText.gameObject.SetActive(_quantity != 1);
        QuantityText.text = $"{Quantity}";
      }
    }

    public void Initialize(ItemStaticData itemStaticData, int quantity)
    {
      Id = itemStaticData.Id;
      Weight = itemStaticData.Weight;
      MaxQuantityInStack = itemStaticData.MaxQuantityInStack;
      Quantity = quantity;
      Icon.sprite = itemStaticData.Icon;
    }
  }
}