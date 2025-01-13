using System.Collections.Generic;
using Photon.Realtime;

namespace Business.UI.RoomList.Interfaces
{
    public interface IRoomListModel
    {
        void ReconnectToLobby();
        List<RoomInfo> UpdateRoomList(List<RoomInfo> roomList);
    }
}