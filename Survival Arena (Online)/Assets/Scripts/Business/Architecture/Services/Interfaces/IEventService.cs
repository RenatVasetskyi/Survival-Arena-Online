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
        event Action OnJoinedLobby;
        void SendPhotonConnectedToMaster();
        void SendJoinedRoom();
        void SendJoinedLobby();
        void SendRoomListUpdated(List<RoomInfo> roomList);
    }
}