using System;
using Code.UI.Factories;
using UnityEngine;

namespace Code.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IHudFactory _hudFactory;
    private readonly IInventoryFactory _inventoryFactory;
    private readonly ISlotFactory _slotFactory;

    public LoadLevelState(
      GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      IHudFactory hudFactory,
      IInventoryFactory inventoryFactory,
      ISlotFactory slotFactory
    )
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
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
      InitializeInventory(hud);
      InitializeSlots();
    }

    private void InitializeSlots()
    {
    }

    private GameObject InitializeHud()
    {
      return _hudFactory.CreateHud();
    }

    private void InitializeInventory(GameObject hud)
    {
      _inventoryFactory.CreateInventory(hud.transform);
    }

    public void Exit()
    {
    }
  }
}