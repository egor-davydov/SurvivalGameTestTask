using System;
using Code.Infrastructure.Actions;
using Code.Infrastructure.AssetManagement;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.ProgressWatchers;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.UI.Factories;
using Code.UI.Services;

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
      RegisterStaticDataService();
      _services.RegisterSingle<IProgressWatchersService>(new ProgressWatchersService());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<ProgressWatchersService>(), _services.Single<PersistentProgressService>()));
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IItemFactory>(new ItemFactory());
      _services.RegisterSingle<IItemService>(new ItemService(_services.Single<IStaticDataService>(), _services.Single<IItemFactory>()));
      _services.RegisterSingle<IHudFactory>(new HudFactory(_services.Single<IAssetProvider>(), _services.Single<IItemService>()));
      _services.RegisterSingle<ISlotFactory>(new SlotFactory(_services.Single<IAssetProvider>()));
      _services.RegisterSingle<IInventoryFactory>(new InventoryFactory(_services.Single<IAssetProvider>()));
    }

    private void RegisterStaticDataService()
    {
      StaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();
      _services.RegisterSingle<IStaticDataService>(staticDataService);
    }
  }
}