using Business.Game.EnemyLogic.Interfaces;
using Business.Game.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Business.Architecture.Services.Factories.Interfaces
{
    public interface IGameFactory
    {
        UniTask<IMap> CreateMap();
        IPlayer CreatePlayer(Transform center, Quaternion rotation, Transform parent);
        IEnemy CreateEnemy(Transform center, Quaternion rotation, Transform parent);
        Camera CreateCamera();
    }
}