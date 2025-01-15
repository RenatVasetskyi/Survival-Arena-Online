using System.Threading.Tasks;
using Business.UI.Interfaces;
using Business.UI.RoomList.Interfaces;
using UnityEngine;

namespace Business.Architecture.Services.Interfaces
{
    public interface IUIFactory
    {
        ILoadingCurtain LoadingCurtain { get; }
        Task CreateLoadingCurtain();
        Task<IRoomButton> CreateRoomButton(Transform parent);
        Task CreateMainMenu(Transform parent);
        void MakeLoadingCurtainNull();
    }
}