using System;
using Code.UI.Factories;

namespace Code.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IHudFactory _hudFactory;

    public LoadLevelState(
      GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      IHudFactory hudFactory
    )
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _hudFactory = hudFactory;
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
      _hudFactory.CreateHud();
    }

    public void Exit()
    {
    }
  }
}