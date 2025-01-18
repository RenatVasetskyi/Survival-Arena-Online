using Business.Architecture.Services.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Business.Architecture.Services
{
    using IFactory = Interfaces.IFactory;

    public class BaseFactory : IFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;
        private readonly IPhotonService _photonService;

        protected BaseFactory(IInstantiator instantiator, IAssetProvider assetProvider,
            DiContainer container, IPhotonService photonService)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _container = container;
            _photonService = photonService;
        }

        public T CreateBaseWithContainer<T>(string path) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_assetProvider.Load<T>(path));
        }

        public T CreateBaseWithContainer<T>(string path, Transform parent) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_assetProvider.Load<T>(path), parent);
        }

        public T CreateBaseWithContainer<T>(string path, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(_assetProvider
                    .Load<T>(path), at, rotation, parent);
        }

        public T CreateBaseWithContainer<T>(T prefab, Vector3 at, Quaternion rotation, Transform parent) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, at, rotation, parent);
        }

        public GameObject CreateBaseWithContainer(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent)
        {
            return _instantiator.InstantiatePrefab(prefab, at, rotation, parent);
        }

        public T CreateBaseWithObject<T>(string path) where T : Component
        {
            return Object.Instantiate(_assetProvider.Load<T>(path));
        }

        public GameObject CreateBaseWithContainer(string path, Transform parent)
        {
            return _instantiator.InstantiatePrefab(_assetProvider.Load<GameObject>(path), parent);
        }
        
        public async UniTask<GameObject> CreateAddressableWithContainer
            (AssetReferenceGameObject assetReference, Vector3 at, Quaternion rotation, Transform parent)
        {
            GameObject loadedResource = await _assetProvider.Load<GameObject>(assetReference);

            return _instantiator.InstantiatePrefab(loadedResource, at, rotation, parent);
        }

        public async UniTask<GameObject> CreateAddressableWithObject(AssetReferenceGameObject assetReference,
            Vector3 at, Quaternion rotation, Transform parent)
        {
            GameObject loadedResource = await _assetProvider.Load<GameObject>(assetReference);
            
            return Object.Instantiate(loadedResource, at, rotation, parent);
        }

        public T CreateWithPhoton<T>(string path, Vector3 at, Quaternion rotation, Transform parent)
        {
            GameObject createdObject = _photonService.Instantiate(path, at, rotation);
            createdObject.transform.SetParent(parent);
            _container.Inject(createdObject);
            return createdObject.GetComponent<T>();
        }
    }
}