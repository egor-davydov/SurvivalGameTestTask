using System;
using Code.Infrastructure.AssetManagement;
using Code.Services;
using Code.Services.StaticData;
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
      _services.RegisterSingle<IStaticDataService>(new StaticDataService());
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IHudFactory>(new HudFactory(_services.Single<IAssetProvider>()));
      _services.RegisterSingle<ISlotFactory>(new SlotFactory(_services.Single<IAssetProvider>()));
      _services.RegisterSingle<IInventoryFactory>(new InventoryFactory(_services.Single<IAssetProvider>()));
    }
  }
}