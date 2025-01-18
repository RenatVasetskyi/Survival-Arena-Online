using System.Threading.Tasks;
using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data;
using Business.Game.Spawn.Interfaces;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Business.Architecture.Services.Factories
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private readonly IPhotonService _photonService;

        protected GameFactory(IInstantiator instantiator, IAssetProvider assetProvider,
            DiContainer container, IPhotonService photonService) : base
            (instantiator, assetProvider, container, photonService)
        {
            _photonService = photonService;
        }

        public async Task<IMap> CreateMap()
        {
            if (_photonService.IsMasterClient)
            {
                PhotonView map = CreateWithPhoton<PhotonView>(AssetPath.Map, Vector3.zero, Quaternion.identity, null);
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { {"map", map.ViewID}});
                return map.GetComponent<IMap>();
            }
            
            return await GetMap();
        }

        private async UniTask<IMap> GetMap()
        {
            await new WaitUntil(() => PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"));
            int viewId = (int)PhotonNetwork.CurrentRoom.CustomProperties["map"];
            await new WaitUntil(() => PhotonView.Find(viewId) != null);
            return PhotonView.Find(viewId).GetComponent<IMap>();
        }
    }
}