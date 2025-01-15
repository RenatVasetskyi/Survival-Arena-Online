using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace Business.Architecture.Services.Interfaces
{
    public interface IEventService
    {
        event Action OnPhotonConnectedToMaster;
        event Action OnJoinedRoom;
        event Action<List<RoomInfo>> OnRoomListUpdated;
        void SendPhotonConnectedToMaster();
        void SendJoinedRoom();
        void SendRoomListUpdated(List<RoomInfo> roomList);
    }
}