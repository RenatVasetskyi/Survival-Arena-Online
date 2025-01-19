using UnityEngine;

namespace Business.Game.Interfaces
{
    public interface IPlayer
    {
        void Initialize(Joystick joystick, Camera camera);
    }
}