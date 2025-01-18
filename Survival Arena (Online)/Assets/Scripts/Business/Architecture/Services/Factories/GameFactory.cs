using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data;
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

        public void CreateMap()
        {
            if (_photonService.IsMasterClient)
            {
                CreateWithPhoton<PhotonView>(AssetPath.Map, Vector3.zero, Quaternion.identity, null);
            }
        }
    }
}