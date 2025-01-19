using Business.Game.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Mono.Game.UI
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _attackButton;
        
        public Joystick Joystick => _joystick;
        public Button AttackButton => _attackButton;
    }
}