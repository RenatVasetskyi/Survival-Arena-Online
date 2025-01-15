using System;
using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Photon.Realtime;

namespace Business.Architecture.Services
{
    public class EventService : IEventService
    {
        public event Action OnPhotonConnectedToMaster;
        public event Action OnJoinedRoom;
        public event Action<List<RoomInfo>> OnRoomListUpdated;

        public void SendPhotonConnectedToMaster()
        {
            OnPhotonConnectedToMaster?.Invoke();
        }
        
        public void SendRoomListUpdated(List<RoomInfo> roomList)
        {
            OnRoomListUpdated?.Invoke(roomList); 
        }

        public void SendJoinedRoom()
        {
            OnJoinedRoom?.Invoke();
        }
    }
}