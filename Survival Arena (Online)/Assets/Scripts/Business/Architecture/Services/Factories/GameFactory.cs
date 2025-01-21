using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data;
using Business.Data.Interfaces;
using Business.Game.EnemyLogic.Interfaces;
using Business.Game.Interfaces;
using Business.Math;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Business.Architecture.Services.Factories
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private const string MapKey = "Map";
        
        private readonly IPhotonService _photonService;
        private readonly IGameSettings _gameSettings;

        public IMap Map { get; private set; }
        
        protected GameFactory(IInstantiator instantiator, IAssetProvider assetProvider,
            DiContainer container, IPhotonService photonService, IGameSettings gameSettings) : base
            (instantiator, assetProvider, container, photonService)
        {
            _photonService = photonService;
            _gameSettings = gameSettings;
        }

        public async UniTask<IMap> CreateMap()
        {
            if (_photonService.IsMasterClient)
            {
                PhotonView map = CreateWithPhoton<PhotonView>(AssetPath.Map, Vector3.zero, Quaternion.identity, null);
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { { MapKey, map.ViewID } } );
                return Map = map.GetComponent<IMap>();
            }
            
            return await GetMap();
        }

        public IPlayer CreatePlayer(Transform center, Quaternion rotation, Transform parent)
        {
            return CreateWithPhoton<PhotonView>(AssetPath.Player, ProjectMath.GetPointInCircle
                (center, Map.CastleRadius, Map.DefenceZoneRadius),
                rotation, parent).GetComponent<IPlayer>();
        }

        public IEnemy CreateEnemy(Transform center, Quaternion rotation, Transform parent)
        {
            return CreateWithPhoton<PhotonView>(AssetPath.Enemy, ProjectMath.GetPointInCircle
                    (center, _gameSettings.EnemyMinSpawnRange, _gameSettings.EnemyMaxSpawnRange),
                rotation, parent).GetComponent<IEnemy>();
        }
        
        public Camera CreateCamera()
        {
            return CreateBaseWithObject<Camera>(AssetPath.GameCamera, null);
        }

        private async UniTask<IMap> GetMap()
        {
            await new WaitUntil(() => PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MapKey));
            int viewId = (int)PhotonNetwork.CurrentRoom.CustomProperties[MapKey];
            await new WaitUntil(() => PhotonView.Find(viewId) != null);
            return Map = PhotonView.Find(viewId).GetComponent<IMap>();
        }
    }
}