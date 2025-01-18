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
            float randomRadius = Random.Range(_gameSettings.PlayerMinSpawnRange, _gameSettings.PlayerMaxSpawnRange);
            Vector2 randomPoint = Random.insideUnitCircle * randomRadius;
            
            Vector3 spawnPosition = new Vector3(
                middlePoint.position.x + randomPoint.x,
                middlePoint.position.y,
                middlePoint.position.z + randomPoint.y
            );

            return CreateWithPhoton<PhotonView>(AssetPath.Player, spawnPosition, Quaternion.identity, parent).GetComponent<IPlayer>();
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