using Business.UI.Interfaces;
using Business.UI.RoomList.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Business.Architecture.Services.Interfaces
{
    public interface IUIFactory
    {
        ILoadingCurtain LoadingCurtain { get; }
        UniTask CreateLoadingCurtain();
        UniTask<IRoomButton> CreateRoomButton(Transform parent);
        UniTask CreateMainMenu(Transform parent);
        void MakeLoadingCurtainNull();
    }
}