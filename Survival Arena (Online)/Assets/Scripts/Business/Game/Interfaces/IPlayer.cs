using Mono.Game.Character.Interfaces;
using UnityEngine;

namespace Business.Game.Interfaces
{
    public interface IPlayer
    {
        void Initialize(IInputController inputController, Camera camera);
    }
}