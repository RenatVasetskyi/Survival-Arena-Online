using Business.Architecture.Services.Factories.Interfaces;
using Business.Game.PlayerLogic.Interfaces;
using Business.Game.UI.Interfaces;

namespace Business.Game.Interfaces
{
    public interface IPlayer
    {
        void Initialize(IInputController inputController, IGameView gameView,
            IGameFactory gameFactory);
    }
}