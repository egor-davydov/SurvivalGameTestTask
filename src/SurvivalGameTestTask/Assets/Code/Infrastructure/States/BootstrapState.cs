using System;
using Code.Services;
using Code.UI.Factories;

namespace Code.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, AllServices services)
    {
      _stateMachine = stateMachine;
      _services = services;
      RegisterServices();
    }

    public void Enter()
    {
      _stateMachine.Enter<LoadLevelState, string>("Main");
    }

    public void Exit()
    {
      
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IHudFactory>(new HudFactory());
    }
  }
}