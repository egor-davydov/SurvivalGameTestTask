using System;

namespace Code.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    public LoadLevelState(GameStateMachine stateMachine)
    {
      
    }

    public void Enter(string sceneName)
    {
    }

    public void Exit()
    {
    }
  }
}