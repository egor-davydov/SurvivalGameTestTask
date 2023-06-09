using Code.Infrastructure.AssetManagement;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.ProgressWatchers;
using Code.Services.Randomizer;
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
      _stateMachine.Enter<LoadProgressState>();
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      RegisterStaticDataService();
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IRandomService>(new RandomService());
      _services.RegisterSingle<IProgressWatchersService>(new ProgressWatchersService());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<IItemFactory>(new ItemFactory(_services.Single<IProgressWatchersService>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
        _services.Single<IProgressWatchersService>(),
        _services.Single<IPersistentProgressService>()
      ));
      _services.RegisterSingle<ISlotFactory>(new SlotFactory(
        _services.Single<IAssetProvider>(),
        _services.Single<IProgressWatchersService>(),
        _services.Single<IStaticDataService>()
      ));
      _services.RegisterSingle<IInventoryService>(new InventoryService(
        _services.Single<IPersistentProgressService>(),
        _services.Single<ISlotFactory>()
      ));
      _services.RegisterSingle<IItemService>(new ItemService(
        _services.Single<IStaticDataService>(), 
        _services.Single<IInventoryService>(), _services.Single<IRandomService>()));
      _services.RegisterSingle<ISlotService>(new SlotService(
        _services.Single<IStaticDataService>(),
        _services.Single<IPersistentProgressService>(),
        _services.Single<IInventoryService>()
      ));
      _services.RegisterSingle<IHudFactory>(new HudFactory(
        _services.Single<IAssetProvider>(),
        _services.Single<IItemService>(),
        _services.Single<IProgressWatchersService>()
      ));
      _services.Single<ISlotFactory>().Initialize(_services.Single<ISlotService>());
      _services.RegisterSingle<IInventoryFactory>(new InventoryFactory(
        _services.Single<IAssetProvider>(),
        _services.Single<IProgressWatchersService>(),
        _services.Single<IStaticDataService>(),
        _services.Single<IItemFactory>(),
        _services.Single<ISaveLoadService>()
        ));
    }

    private void RegisterStaticDataService()
    {
      StaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();
      _services.RegisterSingle<IStaticDataService>(staticDataService);
    }
  }
}