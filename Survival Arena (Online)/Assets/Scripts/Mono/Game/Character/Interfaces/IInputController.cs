using System;
using UnityEngine;

namespace Mono.Game.Character.Interfaces
{
    public interface IInputController
    {
        event Action OnInputActivated;
        event Action OnInputDeactivated;
        Vector2 CurrentDirection { get; }
    }
}