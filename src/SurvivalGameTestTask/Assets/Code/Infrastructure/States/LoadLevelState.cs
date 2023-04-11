using System;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factories;
using Code.UI.InventoryWithSlots;
using UnityEngine;

namespace Code.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IStaticDataService _staticData;
    private readonly IHudFactory _hudFactory;
    private readonly IInventoryFactory _inventoryFactory;
    private readonly ISlotFactory _slotFactory;

    public LoadLevelState(
      GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      IStaticDataService staticData,
      IHudFactory hudFactory,
      IInventoryFactory inventoryFactory,
      ISlotFactory slotFactory
    )
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
      _hudFactory = hudFactory;
      _inventoryFactory = inventoryFactory;
      _slotFactory = slotFactory;
    }

    public void Enter(string sceneName)
    {
      _sceneLoader.Load(sceneName, OnLevelLoaded);
    }

    private void OnLevelLoaded()
    {
      InitializeLevel();
    }

    private void InitializeLevel()
    {
      GameObject hud = InitializeHud();
      Inventory inventory = InitializeInventory(hud);
      InitializeSlots(inventory);
    }

    private void InitializeSlots(Inventory inventory)
    {
      InventoryStaticData inventoryData = _staticData.ForInventory();
      int slotsQuantity = inventoryData.SlotsQuantity - inventoryData.LockedSlotsQuantity;
      for (int slotNumber = 0; slotNumber < slotsQuantity; slotNumber++) 
        _slotFactory.CreateSlot(inventory.SlotsParent);
    }

    private GameObject InitializeHud()
    {
      return _hudFactory.CreateHud();
    }

    private Inventory InitializeInventory(GameObject hud)
    {
      Inventory inventory = _inventoryFactory.CreateInventory(hud.transform);
      return inventory;
    }

    public void Exit()
    {
    }
  }
}