using System;
using System.Collections.Generic;
using Code.Services;
using Code.Services.StaticData;
using Code.UI.Factories;

namespace Code.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;

    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader, AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, services),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IStaticDataService>(), services.Single<IHudFactory>(),
          services.Single<IInventoryFactory>(), services.Single<ISlotFactory>()),
      };
    }

    public void Enter<TState>() where TState : IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
    {
      IPayloadState<TPayload> state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : IExitableState
    {
      _currentState?.Exit();
      TState state = (TState)_states[typeof(TState)];
      _currentState = state;

      return state;
    }
  }
}