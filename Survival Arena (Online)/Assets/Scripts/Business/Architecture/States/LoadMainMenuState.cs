using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;
using Business.Data;
using UnityEngine;

namespace Business.Architecture.States
{
    public class LoadMainMenuState : IState
    {
        private const string MainMenuScene = "Main Menu";
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IAudioService _audioService;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IFactory _factory;
        private readonly IPhotonService _photonService;

        public LoadMainMenuState(ISceneLoader sceneLoader, IAudioService audioService, 
            IUIFactory uiFactory, IAssetProvider assetProvider, IFactory factory, 
            IPhotonService photonService)
        {
            _sceneLoader = sceneLoader;
            _audioService = audioService;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _factory = factory;
            _photonService = photonService;
        }
        
        public void Exit()
        {
            _assetProvider.CleanUp();
            _audioService.StopMusic();
            _photonService.LeaveLobby();
        }

        public void Enter()
        {
            _uiFactory.CreateLoadingCurtain();
            _uiFactory.LoadingCurtain.Show();
            _sceneLoader.Load(MainMenuScene, Initialize);
        }

        private void Initialize()
        {
            Transform container = _factory.CreateBaseWithObject<Transform>(AssetPath.Container);
            _uiFactory.CreateMainMenu(container);
            _uiFactory.LoadingCurtain.Hide();
        }
    }
}