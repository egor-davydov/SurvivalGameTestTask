using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Services.ProgressWatchers;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.UI.Factories;
using UnityEngine;

namespace Code.UI.InventoryWithSlots
{
  public class Inventory : MonoBehaviour, IProgressReader
  {
    [SerializeField]
    private Transform _slotsParent;

    private IStaticDataService _staticData;
    private IItemFactory _itemFactory;
    private ISaveLoadService _saveLoadService;

    private InventoryData _progressInventoryData;

    public Transform SlotsParent => _slotsParent;
    public List<InventorySlot> Slots { get; private set; }
    public Queue<LockedSlot> LockedSlots { get; private set; }
    public List<KeyValuePair<int, ItemData>> OccupiedSlots => _progressInventoryData.OccupiedSlots.Dictionary.ToList();

    public void Construct(IStaticDataService staticData, IItemFactory itemFactory, ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
      _itemFactory = itemFactory;
      _staticData = staticData;
    }

    public void Initialize(List<InventorySlot> slots, Queue<LockedSlot> lockedSlots)
    {
      LockedSlots = lockedSlots;
      Slots = slots;
    }

    private void Start()
    {
      FillInventoryWithItems();
      _progressInventoryData.Changed += RefreshSlots;
    }

    public void ReceiveProgress(PlayerProgress progress) =>
      _progressInventoryData = progress.InventoryData;

    private void RefreshSlots()
    {
      ClearInventory();
      FillInventoryWithItems();
      _saveLoadService.SaveProgress();
    }

    private void ClearInventory()
    {
      for (int slotNumber = 0; slotNumber < Slots.Count; slotNumber++)
        ClearSlot(slotNumber);
    }

    private void ClearSlot(int slotNumber)
    {
      if (!Slots[slotNumber].IsEmpty)
        Slots[slotNumber].RemoveItem();
    }

    private void FillInventoryWithItems()
    {
      foreach (KeyValuePair<int, ItemData> itemData in OccupiedSlots)
      {
        InventorySlot inventorySlot = Slots[itemData.Key];
        InventoryItem inventoryItem = _itemFactory.CreateItem(inventorySlot.transform);
        inventorySlot.AddItem(inventoryItem);
        inventoryItem.Initialize(_staticData.ForItem(itemData.Value.Id), itemData.Value.Quantity);
      }
    }
  }
}