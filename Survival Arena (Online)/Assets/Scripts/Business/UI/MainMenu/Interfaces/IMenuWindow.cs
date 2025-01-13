using Business.UI.MainMenu.Mediator.Enums;
using UnityEngine;

namespace Business.UI.MainMenu.Interfaces
{
    public interface IMenuWindow
    {
        MainMenuWindowType Type { get; }
        GameObject GameObject { get; }
    }
}