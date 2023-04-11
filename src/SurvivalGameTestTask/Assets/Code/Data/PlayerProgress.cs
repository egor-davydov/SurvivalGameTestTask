using System;

namespace Code.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public InventoryData InventoryData;

    public PlayerProgress()
    {
      InventoryData = new InventoryData();
    }
  }
}