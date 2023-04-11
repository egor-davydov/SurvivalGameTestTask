using Code.Infrastructure.States;
using UnityEngine;

namespace Code.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    private void Awake()
    {
      GameStateMachine stateMachine = new GameStateMachine(new SceneLoader(this));
      stateMachine.Enter<BootstrapState>();
    }
  }
}
