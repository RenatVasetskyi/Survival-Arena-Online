using UnityEngine;

namespace Business.Game.Spawn.Interfaces
{
    public interface IPlayerSpawner
    {
        void SpawnPlayerInRange(Transform middlePoint, float range);
    }
}