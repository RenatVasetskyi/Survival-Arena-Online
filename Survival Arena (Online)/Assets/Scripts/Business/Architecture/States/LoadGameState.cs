using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;

namespace Business.Architecture.States
{
    public class LoadGameState : IState
    {
        private const string GameScene = "Game";
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGamePauser _gamePauser;
        private readonly IAudioService _audioService;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;

        public LoadGameState(ISceneLoader sceneLoader, IGamePauser gamePauser,
            IAudioService audioService, IUIFactory uiFactory, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
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
            
            _sceneLoader.Load(GameScene, Initialize);
        }

        private async void Initialize()
        {
            _gamePauser.Clear();
            _gamePauser.SetPause(false);
        }
    }
}