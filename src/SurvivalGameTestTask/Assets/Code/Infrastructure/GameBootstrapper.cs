using Code.Infrastructure.States;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    private void Awake()
    {
      GameStateMachine stateMachine = new GameStateMachine(new SceneLoader(this), new AllServices());
      stateMachine.Enter<BootstrapState>();
      DontDestroyOnLoad(this);
    }
  }
}
