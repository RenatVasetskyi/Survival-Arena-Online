using Business.Architecture.Services.Interfaces;
using Business.UI.RoomList;
using Business.UI.RoomList.Interfaces;
using UnityEngine;
using Zenject;

namespace Mono.UI.Menu.Rooms
{
    public class RoomList : MonoBehaviour
    {
        private IRoomListPresenter _presenter;
        
        [Inject]
        public void Inject(IUIFactory uiFactory, IPhotonService photonService)
        {
            _presenter = new RoomListPresenter(uiFactory, photonService);
        }
    }
}