using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
      _progressService.Progress = 
        _saveLoadService.LoadProgress()
        ?? NewProgress();
      _stateMachine.Enter<LoadLevelState, string>("Main");
    }

    private PlayerProgress NewProgress()
    {
      PlayerProgress playerProgress = new PlayerProgress();
      return playerProgress;
    }

    public void Exit()
    {
    }
  }
}