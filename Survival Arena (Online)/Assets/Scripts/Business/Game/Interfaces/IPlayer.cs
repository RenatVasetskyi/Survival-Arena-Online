using Business.Game.PlayerLogic.Interfaces;
using UnityEngine;

namespace Business.Game.Interfaces
{
    public interface IPlayer
    {
        void Initialize(IInputController inputController);
    }
}