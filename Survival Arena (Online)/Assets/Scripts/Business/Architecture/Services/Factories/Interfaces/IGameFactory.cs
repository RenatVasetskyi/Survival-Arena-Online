using System.Threading.Tasks;
using Business.Game.Spawn.Interfaces;

namespace Business.Architecture.Services.Factories.Interfaces
{
    public interface IGameFactory
    {
        Task<IMap> CreateMap();
    }
}