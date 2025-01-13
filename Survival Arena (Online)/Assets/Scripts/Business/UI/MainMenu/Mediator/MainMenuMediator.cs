using System.Collections.Generic;
using Business.UI.MainMenu.Mediator.Enums;
using Business.UI.MainMenu.Mediator.Interfaces;
using UnityEngine;

namespace Business.UI.MainMenu.Mediator
{
    public class MainMenuMediator : IMainMenuMediator
    {
        private readonly Dictionary<MainMenuWindowType, GameObject> _menuWindows;

        public MainMenuMediator(Dictionary<MainMenuWindowType, GameObject> menuWindows)
        {
            _menuWindows = menuWindows;
        }
        
        public void OpenExclusiveWindow(MainMenuWindowType windowType)
        {
            GameObject window = _menuWindows[windowType];
            window.SetActive(true);

            Dictionary<MainMenuWindowType, GameObject> otherWindowsToClose = _menuWindows;
            otherWindowsToClose.Remove(windowType);

            foreach (KeyValuePair<MainMenuWindowType, GameObject> windowToClose in otherWindowsToClose)
                windowToClose.Value.SetActive(false);
        }
    }
}