using Business.Game.Interfaces;
using UnityEngine;

namespace Mono.Game
{
    public class Map : MonoBehaviour, IMap
    {
        [SerializeField] private Transform _defenceZone;
        public Transform DefenceZone => _defenceZone;
    }
}
