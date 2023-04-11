using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.StaticData;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string InventoryStaticDataPath = "StaticData/Inventory/InventoryStaticData";
    private const string ItemStaticDataPath = "StaticData/Items";

    private InventoryStaticData _inventory;
    private Dictionary<string, ItemStaticData> _items;


    public void Load()
    {
      
      _inventory = Resources
        .Load<InventoryStaticData>(InventoryStaticDataPath);
      _items = Resources
        .LoadAll<ItemStaticData>(ItemStaticDataPath)
        .ToDictionary(x => x.Id, x => x);

      
    }

    public ItemStaticData ForItem(string id)
    {
      return _items.TryGetValue(id, out ItemStaticData itemStaticData)
        ? itemStaticData
        : throw new ApplicationException(
          $"No {((MethodInfo)MethodInfo.GetCurrentMethod()).ReturnType.Name} with id {id}. " +
          $"Add necessary {((MethodInfo)MethodInfo.GetCurrentMethod()).ReturnType.Name} to Resources/{ItemStaticDataPath}");
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