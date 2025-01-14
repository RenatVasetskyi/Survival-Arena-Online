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

        public LoadMainMenuState(ISceneLoader sceneLoader, IAudioService audioService, 
            IUIFactory uiFactory, IAssetProvider assetProvider, IFactory factory)
        {
            _sceneLoader = sceneLoader;
            _audioService = audioService;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _factory = factory;
        }
        
        public void Exit()
        {
            _assetProvider.CleanUp();
            
            _audioService.StopMusic();
        }

        public void Enter()
        {
            _sceneLoader.Load(MainMenuScene, Initialize);
        }

        private void Initialize()
        {
            _factory.CreateBaseWithObject<Transform>(AssetPath.Container);
            _uiFactory.CreateMainMenu();
            
            if (_uiFactory.LoadingCurtain.GameObject != null)
                _uiFactory.LoadingCurtain.Hide();
        }
    }
}