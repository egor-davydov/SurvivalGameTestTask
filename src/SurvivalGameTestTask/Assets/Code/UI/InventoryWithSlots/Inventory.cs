using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Services.ProgressWatchers;
using Code.Services.StaticData;
using Code.UI.Factories;
using UnityEngine;

namespace Code.UI.InventoryWithSlots
{
  public class Inventory : MonoBehaviour, IProgressReader, IProgressWriter
  {
    [SerializeField]
    private Transform _slotsParent;

    private List<InventorySlot> _slots;
    private InventoryData _progressInventoryData;
    private IItemFactory _itemFactory;
    private IStaticDataService _staticData;
    private Queue<LockedSlot> _lockedSlots;

    public Transform SlotsParent
    {
      get => _slotsParent;
      set => _slotsParent = value;
    }

    private List<InventorySlot> OccupiedSlots => _slots.Where(x => !x.IsEmpty).ToList();
    
    public void Construct(IStaticDataService staticData, IItemFactory itemFactory)
    {
      _itemFactory = itemFactory;
      _staticData = staticData;
    }

    public void Initialize(List<InventorySlot> slots, Queue<LockedSlot> lockedSlots)
    {
      _lockedSlots = lockedSlots;
      _slots = slots;
    }

    private void Start() =>
      FillInventoryFromProgress();

    public void ReceiveProgress(PlayerProgress progress) =>
      _progressInventoryData = progress.InventoryData;

    public void UpdateProgress(PlayerProgress progress)
    {
      InventoryData progressInventoryData = progress.InventoryData;
      foreach (InventorySlot inventorySlot in OccupiedSlots)
        progressInventoryData.AddOccupiedSlot(
          inventorySlot.SlotNumber,
          new ItemData(inventorySlot.ItemId, inventorySlot.Quantity));

      foreach (int occupiedSlotNumber in progressInventoryData.OccupiedSlots.Dictionary.Keys.Where(slotNumber => _slots[slotNumber].IsEmpty).ToList())
        progressInventoryData.Remove(occupiedSlotNumber);
    }
    
    public void MakeNextSlotUnlockable() =>
      _lockedSlots.Dequeue().MakeUnlockable();

    public void AddItem(InventoryItem item)
    {
      foreach (InventorySlot slot in _slots)
      {
        if (slot.IsEmpty)
        {
          slot.AddItem(item);
          return;
        }

        if (SameNotFullItem(slot, item))
        {
          if (SlotNotFitsAllQuantity(item, slot))
          {
            TradeQuantityForStack(item, slot);

            AddItem(item);
            return;
          }

          Destroy(item.gameObject);
          slot.IncreaseQuantity(item.Quantity);
          return;
        }
      }

      Destroy(item.gameObject);
      Debug.LogError("Cant AddItem. Inventory is full");
    }

    public void AddSlot(InventorySlot inventorySlot) =>
      _slots.Add(inventorySlot);

    
    public void DecreaseItemQuantity(string id, int quantity, int slotNumberToStartSearch = 0)
    {
      for (int slotNumber = slotNumberToStartSearch; slotNumber < OccupiedSlots.Count; slotNumber++)
      {
        InventorySlot inventorySlot = OccupiedSlots[slotNumber];
        if (inventorySlot.ItemId == id)
        {
          if (inventorySlot.Quantity <= quantity)
          {
            ClearSlot(slotNumber);
            if (inventorySlot.Quantity < quantity)
              DecreaseItemQuantity(id, quantity - inventorySlot.Quantity, slotNumber + 1);
          }
          else
            inventorySlot.DecreaseQuantity(quantity);

          return;
        }
      }
    }

    public void ClearRandomSlot()
    {
      List<InventorySlot> occupiedSlots = OccupiedSlots;
      if (occupiedSlots.Count == 0)
      {
        Debug.LogError("No items in inventory");
        return;
      }

      ClearSlot(Random.Range(0, occupiedSlots.Count));
    }

    private void ClearSlot(int slotNumber) =>
      OccupiedSlots[slotNumber].RemoveItem();

    private void FillInventoryFromProgress()
    {
      foreach (KeyValuePair<int, ItemData> itemData in _progressInventoryData.OccupiedSlots.Dictionary)
      {
        InventorySlot inventorySlot = _slots[itemData.Key];
        InventoryItem inventoryItem = _itemFactory.CreateItem(inventorySlot.transform);
        inventorySlot.AddItem(inventoryItem);
        inventoryItem.Initialize(_staticData.ForItem(itemData.Value.Id), itemData.Value.Quantity);
      }
    }
    
    private bool SameNotFullItem(InventorySlot slot, InventoryItem item) =>
      !slot.IsEmpty && SameType(slot, item) && !slot.IsFull;

    private bool SlotNotFitsAllQuantity(InventoryItem item, InventorySlot slot) =>
      item.Quantity + slot.Quantity > slot.Capacity;

    private void TradeQuantityForStack(InventoryItem item, InventorySlot slot)
    {
      int quantityNeededForStack = slot.Capacity - slot.Quantity;
      slot.IncreaseQuantity(quantityNeededForStack);
      item.DecreaseQuantity(quantityNeededForStack);
    }

    private bool SameType(InventorySlot slot, InventoryItem item) =>
      slot.ItemId == item.Id;
    
    public int QuantityOf(string id) =>
      OccupiedSlots.Sum(x => x.QuantityOf(id));
  }
}