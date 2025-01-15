using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Business.Architecture.Services.Interfaces
{
    public interface IAssetProvider
    {
        UniTask InitializeAddressable();
        T Load<T>(string path) where T : Object;
        UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : Object;
        void CleanUp();
    }
}
