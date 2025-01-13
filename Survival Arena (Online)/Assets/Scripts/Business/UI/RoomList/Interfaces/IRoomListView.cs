using System.Collections.Generic;
using Photon.Realtime;

namespace Business.UI.RoomList.Interfaces
{
    public interface IRoomListView
    {
        void UpdateRoomList(List<RoomInfo> roomList);
    }
}