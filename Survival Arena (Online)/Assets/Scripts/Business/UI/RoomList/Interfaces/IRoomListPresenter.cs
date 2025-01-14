using System.Collections.Generic;
using Photon.Realtime;

namespace Business.UI.RoomList.Interfaces
{
    public interface IRoomListPresenter
    {
        void UpdateRoomList(List<RoomInfo> roomList);
        void CreateRoom(string name);
    }
}