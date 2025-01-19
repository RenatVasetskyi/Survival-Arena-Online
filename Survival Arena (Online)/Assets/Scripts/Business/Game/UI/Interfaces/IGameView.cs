using UnityEngine.UI;

namespace Business.Game.UI.Interfaces
{
    public interface IGameView
    {
        Joystick Joystick { get; }
        Button AttackButton { get; }
    }
}