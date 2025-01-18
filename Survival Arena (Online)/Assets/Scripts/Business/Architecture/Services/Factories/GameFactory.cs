using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Zenject;

namespace Business.Architecture.Services.Factories
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        protected GameFactory(IInstantiator instantiator, IAssetProvider assetProvider,
            DiContainer container, IPhotonService photonService) : base(instantiator, assetProvider, container, photonService) { }


        // public GameObject CreateMap()
        // {
            // CreateWithPhoton<>();
        // }
    }
}