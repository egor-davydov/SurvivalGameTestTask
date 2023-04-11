using Code.Infrastructure.States;
using UnityEngine;

namespace Code.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour
  {
    private void Awake()
    {
      GameStateMachine stateMachine = new GameStateMachine();
    }
  }
}
