using System;
using UnityEngine;

namespace Business.Data
{
    [Serializable]
    public class GameObjectHolder
    {
        [SerializeField] private GameObject _loadingCurtain;
        [SerializeField] private GameObject _roomButton;
        [SerializeField] private GameObject _mainMenu;
        public GameObject LoadingCurtain => _loadingCurtain;
        public GameObject RoomButton => _roomButton;
        public GameObject MainMenu => _mainMenu;
    }
}