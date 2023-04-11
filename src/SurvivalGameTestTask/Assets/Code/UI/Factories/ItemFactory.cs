using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public class ItemFactory : IItemFactory
  {
    private const string ItemPath = "Hud/Inventory/Item";
    
    public InventoryItem CreateItem(Transform parent)
    {
      GameObject itemPrefab = Resources.Load<GameObject>(ItemPath);
      GameObject itemObject = Object.Instantiate(itemPrefab, parent);
      InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();

      return inventoryItem;
    }
  }
}