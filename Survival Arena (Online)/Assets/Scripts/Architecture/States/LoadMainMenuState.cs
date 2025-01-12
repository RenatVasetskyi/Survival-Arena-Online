using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;

namespace Architecture.States
{
    public class LoadMainMenuState : IState
    {
        private const string MainMenuScene = "MainMenu";
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IAudioService _audioService;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;

        public LoadMainMenuState(ISceneLoader sceneLoader, IAudioService audioService, 
            IUIFactory uiFactory, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _audioService = audioService;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
        }
        
        public void Exit()
        {
            _assetProvider.CleanUp();
            
            _audioService.StopMusic();
        }

        public void Enter()
        {
            _uiFactory.CreateLoadingCurtain();

            _sceneLoader.Load(MainMenuScene, Initialize);
        }

        private async void Initialize()
        {
        }
    }
}