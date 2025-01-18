using Business.UI.Interfaces;
using Business.UI.RoomList.Interfaces;
using UnityEngine;

namespace Business.Architecture.Services.Factories.Interfaces
{
    public interface IUIFactory
    {
        ILoadingCurtain LoadingCurtain { get; }
        ILoadingCurtain CreateLoadingCurtain();
        IRoomButton CreateRoomButton(Transform parent);
        void CreateMainMenu(Transform parent);
        void MakeLoadingCurtainNull();
    }
}