using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace Business.Architecture.Services.Interfaces
{
    public interface IEventService
    {
        event Action OnPhotonConnectedToMaster;
        event Action OnJoinedLobby;
        event Action OnLeftLobby;
        event Action<List<RoomInfo>> OnRoomListUpdated;
        void SendPhotonConnectedToMaster();
        void SendRoomListUpdated(List<RoomInfo> roomList);
        void SendJoinedLobby();
        void SendLeftLobby();
    }
}