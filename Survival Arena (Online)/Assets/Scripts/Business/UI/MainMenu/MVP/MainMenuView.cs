using System;
using AYellowpaper.SerializedCollections;
using Business.UI.MainMenu.Mediator.Enums;
using Business.UI.MainMenu.MVP.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Business.UI.MainMenu.MVP
{
    [Serializable]
    public class MainMenuView : IMainMenuView
    {
        [SerializeField] private SerializedDictionary<MainMenuWindowType, GameObject> _menuWindows;
        [SerializeField] private Button _selectRoomWindowButton;
        [SerializeField] private Button _closeSelectRoomWindowButton;

        public SerializedDictionary<MainMenuWindowType, GameObject> MenuWindows => _menuWindows;
        public Button SelectRoomWindowButton => _selectRoomWindowButton;
        public Button CloseSelectRoomWindowButton => _closeSelectRoomWindowButton;
    }
}