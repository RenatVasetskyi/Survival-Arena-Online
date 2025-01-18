using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;
using Business.Data.Interfaces;
using Business.Game.Spawn;
using Business.Game.Spawn.Interfaces;

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
        private readonly IPhotonService _photonService;
        private readonly IEventService _eventService;
        private readonly IGameFactory _gameFactory;
        private readonly PlayerSpawner _playerSpawner;

        public LoadGameState(ISceneLoader sceneLoader, IGamePauser gamePauser,
            IAudioService audioService, IUIFactory uiFactory, IAssetProvider assetProvider, 
            IPhotonService photonService, IEventService eventService, IFactory factory, 
            IGameSettings gameSettings, IGameFactory gameFactory)
        {
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
            _audioService = audioService;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _photonService = photonService;
            _eventService = eventService;
            _gameFactory = gameFactory;
            _playerSpawner = new PlayerSpawner(factory, gameSettings);
        }
        
        public void Exit()
        {
            _assetProvider.CleanUp();
            _audioService.StopMusic();
            _eventService.OnJoinedLobby -= JoinOrCreateRoom;
            _eventService.OnJoinedRoom -= Initialize;
        }

        public void Enter()
        { 
            _uiFactory.CreateLoadingCurtain();

            _eventService.OnJoinedLobby += JoinOrCreateRoom;
            _eventService.OnJoinedRoom += Initialize;
            
            _sceneLoader.Load(GameScene, JoinLobby);
        }

        private void JoinLobby()
        {
            _photonService.JoinLobby();
        }

        private void JoinOrCreateRoom()
        {
            _photonService.JoinOrCreateRoom(_photonService.ConnectionRoomName);
        }

        private async void Initialize()
        {
            _gamePauser.Clear();
            _gamePauser.SetPause(false);
            
            IMap map = await _gameFactory.CreateMap();
            _playerSpawner.SpawnPlayerInRange(map.SpawnPoint, null);
            
            _uiFactory.LoadingCurtain.Hide();
        }
    }
}