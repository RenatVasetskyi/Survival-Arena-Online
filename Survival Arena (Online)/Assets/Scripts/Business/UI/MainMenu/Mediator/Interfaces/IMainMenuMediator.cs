using Business.UI.MainMenu.Mediator.Enums;

namespace Business.UI.MainMenu.Mediator.Interfaces
{
    public interface IMainMenuMediator
    {
        void OpenExclusiveWindow(MainMenuWindowType windowType);
    }
}