using AYellowpaper.SerializedCollections;
using Business.UI.MainMenu.Mediator.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Business.UI.MainMenu.MVP.Interfaces
{
    public interface IMainMenuView
    {
        SerializedDictionary<MainMenuWindowType, GameObject> MenuWindows { get; }
        Button SelectRoomWindowButton { get; }
        Button CloseSelectRoomWindowButton { get; }
    }
}