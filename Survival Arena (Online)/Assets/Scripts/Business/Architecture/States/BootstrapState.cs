using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;
using Application = UnityEngine.Device.Application;

namespace Business.Architecture.States
{
    public class BootstrapState : IState
    {
        private const string BootstrapSceneName = "Bootstrap";

        private const int TargetFrameRate = 120;
        
        private readonly IStateMachine _stateMachine;
        private readonly IAudioService _audioService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IPhotonService _photonService;
        private readonly IEventService _eventService;

        public BootstrapState(IStateMachine stateMachine, IAudioService audioService, 
            ISceneLoader sceneLoader, IUIFactory uiFactory, IAssetProvider assetProvider, 
            IPhotonService photonService, IEventService eventService)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _photonService = photonService;
            _eventService = eventService;
        }

        public void Exit()
        {
            _eventService.OnPhotonConnectedToMaster -= LoadMainMenu;
        }

        public void Enter()
        {
            _eventService.OnPhotonConnectedToMaster += LoadMainMenu;
            _sceneLoader.Load(BootstrapSceneName, Initialize);
        }

        private async void Initialize()
        {
            _uiFactory.CreateLoadingCurtain();
            
            Application.targetFrameRate = TargetFrameRate;
            
            _audioService.Initialize();
            await _assetProvider.InitializeAddressable();
            _photonService.Reconnect();
        }

        private void LoadMainMenu()
        {
            _stateMachine.Enter<LoadMainMenuState>();
        }
    }
}