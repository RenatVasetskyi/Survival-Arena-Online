using AYellowpaper.SerializedCollections;
using Business.UI.MainMenu.Mediator.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Business.UI.MainMenu.MVP.Interfaces
{
    public interface IMainMenuView
    {
        public SerializedDictionary<MainMenuWindowType, GameObject> MenuWindows { get; }
        public Button SelectRoomWindowButton { get; }
    }
}