using System;

namespace Code.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;

    public BootstrapState(GameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }

    public void Enter()
    {
      _stateMachine.Enter<LoadLevelState, string>("Main");
    }

    public void Exit()
    {
      
    }
  }
}