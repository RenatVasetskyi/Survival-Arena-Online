using UnityEngine;

namespace Business.Game.Interfaces
{
    public interface IMap
    {
        Transform SpawnPoint { get; }
        Transform DefenceZone { get; }
    }
}