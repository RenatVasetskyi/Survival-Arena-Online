using System.Collections.Generic;
using System.Linq;
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
            if (!_menuWindows.ContainsKey(windowType))
            {
                Debug.LogError("Cannot open exclusive window, because dictionary doesn't contain this window type.");
                return;
            }
            
            GameObject window = _menuWindows[windowType];
            window.SetActive(true);

            Dictionary<MainMenuWindowType, GameObject> otherWindowsToClose = _menuWindows
                .ToDictionary(x => x.Key, x => x.Value);
            otherWindowsToClose.Remove(windowType);

            foreach (KeyValuePair<MainMenuWindowType, GameObject> windowToClose in otherWindowsToClose)
                windowToClose.Value.SetActive(false);
        }
    }
}