using UnityEngine;

namespace Business.Game.Spawn.Interfaces
{
    public interface IMap
    {
        Transform SpawnPoint { get; }
    }
}