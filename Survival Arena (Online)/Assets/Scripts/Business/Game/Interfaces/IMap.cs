using UnityEngine;

namespace Business.Game.Interfaces
{
    public interface IMap
    {
        Transform DefenceZone { get; }
        float DefenceZoneRadius { get; }
        float CastleRadius { get; }
    }
}