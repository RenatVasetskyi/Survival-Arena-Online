using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.UI;

namespace Business.UI.RoomList.Interfaces
{
    public interface IRoomListView
    {
        Button CreateRoomButton { get; }
        void UpdateRoomList(List<RoomInfo> roomList);
    }
}