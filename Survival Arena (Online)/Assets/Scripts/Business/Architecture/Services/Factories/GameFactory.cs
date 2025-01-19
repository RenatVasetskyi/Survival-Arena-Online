using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data;
using Business.Data.Interfaces;
using Business.Game.Interfaces;
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
                return map.GetComponent<IMap>();
            }
            
            return await GetMap();
        }

        public IPlayer CreatePlayer(Transform middlePoint, Transform parent)
        {
            //create player in circle
            float angle = Random.Range(0, Mathf.PI * 2);
            float radius = Mathf.Sqrt(Random.Range(_gameSettings.PlayerMinSpawnRange * _gameSettings
                .PlayerMinSpawnRange, _gameSettings.PlayerMaxSpawnRange * _gameSettings.PlayerMaxSpawnRange));

            float x = middlePoint.position.x + radius * Mathf.Cos(angle);
            float z = middlePoint.position.z + radius * Mathf.Sin(angle);
            float y = middlePoint.position.y;

            Vector3 spawnPoint = new Vector3(x, y, z);

            return CreateWithPhoton<PhotonView>(AssetPath.Player, spawnPoint, Quaternion.identity, parent).GetComponent<IPlayer>();
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
            return PhotonView.Find(viewId).GetComponent<IMap>();
        }
    }
}