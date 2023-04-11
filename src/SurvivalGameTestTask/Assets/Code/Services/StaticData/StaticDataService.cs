using System;
using System.Reflection;
using Code.StaticData;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string InventoryStaticDataPath = "StaticData/Inventory/InventoryStaticData";
    
    private InventoryStaticData _inventory;

    public void Load()
    {
      _inventory = Resources
        .Load<InventoryStaticData>(InventoryStaticDataPath);
    }
    
    public InventoryStaticData ForInventory()
    {
      return _inventory != null
        ? _inventory
        : throw new ApplicationException(
          $"No {((MethodInfo)MethodInfo.GetCurrentMethod()).ReturnType.Name}. " +
          $"Add necessary {((MethodInfo)MethodInfo.GetCurrentMethod()).ReturnType.Name} to Resources/{InventoryStaticDataPath}");
    }
  }
}