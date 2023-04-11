using System;

namespace Code.Data
{
  [Serializable]
  public class ItemData
  {
    public string Id;
    public int Quantity;

    public ItemData(string id, int quantity)
    {
      Id = id;
      Quantity = quantity;
    }

    public void IncreaseQuantity(int quantity) => 
      Quantity += quantity;

    public void ChangeData(ItemData itemData)
    {
      Id = itemData.Id;
      Quantity = itemData.Quantity;
    }
  }
}