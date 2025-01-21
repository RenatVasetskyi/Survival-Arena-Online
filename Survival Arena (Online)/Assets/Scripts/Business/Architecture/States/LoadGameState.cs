using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;
using Business.Data;
using Business.Game.EnemyLogic;
using Business.Game.EnemyLogic.Interfaces;
using Business.Game.Interfaces;
using Business.Game.UI.Interfaces;
using UnityEngine;

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
        private readonly IFactory _factory;
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        public LoadGameState(ISceneLoader sceneLoader, IGamePauser gamePauser,
            IAudioService audioService, IUIFactory uiFactory, IAssetProvider assetProvider, 
            IPhotonService photonService, IEventService eventService, IFactory factory, 
            IGameFactory gameFactory, ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
            _audioService = audioService;
            _uiFactory = uiFactory;
            _assetProvider = assetProvider;
            _photonService = photonService;
            _eventService = eventService;
            _factory = factory;
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
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
            
            Transform container = _factory.CreateBaseWithObject<Transform>(AssetPath.Container);
            
            Camera camera = _gameFactory.CreateCamera();
            
            IGameView gameView = _uiFactory.CreateGameView(AssetPath.GameView, container);
            IMap map = await _gameFactory.CreateMap();
            IPlayer player = _gameFactory.CreatePlayer(map.DefenceZone, Quaternion.identity, null);
            player.Initialize(gameView.Joystick, gameView);

            if (_photonService.IsMasterClient)
            {
                IEnemySpawner enemySpawner = new EnemySpawner(_gameFactory, _coroutineRunner, _photonService);
                enemySpawner.Spawn(map.DefenceZone, Quaternion.identity, null);
            }

            _uiFactory.LoadingCurtain.Hide();
        }
    }
}