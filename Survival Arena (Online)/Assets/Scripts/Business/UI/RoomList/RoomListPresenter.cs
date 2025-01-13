using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Business.UI.RoomList.Interfaces;
using Photon.Realtime;

namespace Business.UI.RoomList
{
    public class RoomListPresenter : IRoomListPresenter
    {
        private readonly IRoomListModel _model;
        private readonly IRoomListView _view;

        public RoomListPresenter(IUIFactory uiFactory, IPhotonService photonService)
        {
            _model = new RoomListModel(photonService);
            _view = new RoomListView(uiFactory);
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            _view.UpdateRoomList(_model.UpdateRoomList(roomList));
        }
    }
}