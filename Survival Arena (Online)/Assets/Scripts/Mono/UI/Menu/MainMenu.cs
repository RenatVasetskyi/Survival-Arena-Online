using Business.UI.MainMenu.Mediator;
using Business.UI.MainMenu.Mediator.Enums;
using Business.UI.MainMenu.Mediator.Interfaces;
using Business.UI.MainMenu.MVP;
using UnityEngine;

namespace Mono.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
        
        private IMainMenuMediator _menuMediator;
        
        private void Awake()
        {
            _menuMediator = new MainMenuMediator(_mainMenuView.MenuWindows);
            _menuMediator.OpenExclusiveWindow(MainMenuWindowType.MainWindow);
            _mainMenuView.SelectRoomWindowButton.onClick.AddListener(OpenSelectRoomWindow);
        }
        
        private void OnDestroy()
        {
            _mainMenuView.SelectRoomWindowButton.onClick.RemoveListener(OpenSelectRoomWindow);
        }

        private void OpenSelectRoomWindow()
        {
            _menuMediator.OpenExclusiveWindow(MainMenuWindowType.SelectRoomWindow);
        }
    }
}