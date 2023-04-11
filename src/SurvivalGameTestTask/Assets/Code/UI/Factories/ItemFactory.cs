using Code.Services.ProgressWatchers;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.UI.Factories
{
  public class ItemFactory : IItemFactory
  {
    private const string ItemPath = "Hud/Inventory/Item";
    
    private readonly IProgressWatchersService _progressWatchers;

    public ItemFactory(IProgressWatchersService progressWatchers)
    {
      _progressWatchers = progressWatchers;
    }
    
    public InventoryItem CreateItem(Transform parent)
    {
      GameObject itemPrefab = Resources.Load<GameObject>(ItemPath);
      GameObject itemObject = Object.Instantiate(itemPrefab, parent);
      _progressWatchers.Register(itemObject);
      InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();

      return inventoryItem;
    }
  }
}