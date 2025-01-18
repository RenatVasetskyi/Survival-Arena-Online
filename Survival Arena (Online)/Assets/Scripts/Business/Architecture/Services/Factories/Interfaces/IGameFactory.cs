using Business.Game.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Business.Architecture.Services.Factories.Interfaces
{
    public interface IGameFactory
    {
        UniTask<IMap> CreateMap();
        void CreatePlayer(Transform middlePoint, Transform parent);
    }
}