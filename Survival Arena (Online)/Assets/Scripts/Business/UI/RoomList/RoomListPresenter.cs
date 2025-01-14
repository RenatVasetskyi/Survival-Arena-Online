using System.Collections.Generic;
using Business.UI.RoomList.Interfaces;
using Photon.Realtime;

namespace Business.UI.RoomList
{
    public class RoomListPresenter : IRoomListPresenter
    {
        private readonly IRoomListModel _model;
        private readonly IRoomListView _view;

        public RoomListPresenter(IRoomListModel model, IRoomListView view)
        {
            _model = model;
            _view = view;
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            _view.UpdateRoomList(_model.UpdateRoomList(roomList));
        }

        public void CreateRoom(string name)
        {
            _model.CreateRoom(name);
        }
    }
}