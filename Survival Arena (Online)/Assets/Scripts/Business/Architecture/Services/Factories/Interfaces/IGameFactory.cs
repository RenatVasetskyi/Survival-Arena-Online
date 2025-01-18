using Business.Game.Spawn.Interfaces;
using Cysharp.Threading.Tasks;

namespace Business.Architecture.Services.Factories.Interfaces
{
    public interface IGameFactory
    {
        UniTask<IMap> CreateMap();
    }
}