using System;
using UnityEngine;

namespace Business.Game.PlayerLogic.Interfaces
{
    public interface IInputController
    {
        event Action OnInputActivated;
        event Action OnInputDeactivated;
        Vector2 CurrentDirection { get; }
    }
}