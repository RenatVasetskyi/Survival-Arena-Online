using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Business.UI.RoomList.Interfaces;
using Photon.Realtime;

namespace Business.UI.RoomList
{
    public class RoomListModel : IRoomListModel
    {
        private readonly IPhotonService _photonService;
        private List<RoomInfo> _cachedRooms = new();

        public RoomListModel(IPhotonService photonService)
        {
            _photonService = photonService;
        }
        
        public List<RoomInfo> UpdateRoomList(List<RoomInfo> roomList)
        {
            if (roomList.Count <= 0)
            {
                _cachedRooms = roomList;
                return _cachedRooms;
            }

            foreach (RoomInfo room in roomList)
            {
                for (int i = 0; i < _cachedRooms.Count; i++)
                {
                    if (_cachedRooms[i].Name == room.Name)
                    {
                        List<RoomInfo> newRoomList = _cachedRooms;

                        if (room.RemovedFromList)
                            newRoomList.Remove(room);
                        else
                            newRoomList[i] = room;

                        _cachedRooms = newRoomList;
                    }
                }
            }
            
            return _cachedRooms;
        }

        public void CreateRoom(string name)
        {
            _photonService.CreateRoom(name);
        }
    }
}