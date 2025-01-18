using Business.Game.UI.Interfaces;
using UnityEngine;

namespace Mono.Game.UI
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private Joystick _joystick;
        
        public Joystick Joystick => _joystick;
    }
}