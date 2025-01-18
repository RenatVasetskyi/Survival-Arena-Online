using System.Collections.Generic;
using Photon.Realtime;

namespace Business.UI.RoomList.Interfaces
{
    public interface IRoomListModel
    {
        List<RoomInfo> UpdateRoomList(List<RoomInfo> roomList);
        void JoinOrCreateRoom(string name);
    }
}