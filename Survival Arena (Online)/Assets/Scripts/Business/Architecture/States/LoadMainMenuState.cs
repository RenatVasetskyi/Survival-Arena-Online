using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;

namespace Business.Architecture.States
{
    public class LoadMainMenuState : IState
    {
        private const string MainMenuScene = "Main Menu";
        
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
            _sceneLoader.Load(MainMenuScene, Initialize);
        }

        private void Initialize()
        {
            if (_uiFactory.LoadingCurtain.GameObject != null)
                _uiFactory.LoadingCurtain.Hide();
        }
    }
}