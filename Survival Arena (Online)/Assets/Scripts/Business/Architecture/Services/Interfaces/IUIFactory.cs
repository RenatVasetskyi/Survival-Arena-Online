using Business.UI.Interfaces;

namespace Business.Architecture.Services.Interfaces
{
    public interface IUIFactory
    {
        ILoadingCurtain LoadingCurtain { get; }
        void CreateLoadingCurtain();
    }
}