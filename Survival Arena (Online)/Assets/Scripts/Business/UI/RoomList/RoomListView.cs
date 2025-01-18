using System;
using System.Collections.Generic;
using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.UI.RoomList.Interfaces;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Business.UI.RoomList
{
    [Serializable]
    public class RoomListView : IRoomListView
    {
        private readonly IUIFactory _uiFactory;
        
        [SerializeField] private Transform _contentHolder;
        [SerializeField] private Button _createRoomButton;
        
        public Button CreateRoomButton => _createRoomButton;

        public RoomListView(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            ClearRoomList();
            
            foreach (RoomInfo room in roomList)
            {
                IRoomButton roomButton = _uiFactory.CreateRoomButton(_contentHolder);
                roomButton.Initialize(room.Name, room.PlayerCount.ToString(), room.MaxPlayers.ToString());
            }
        }

        private void ClearRoomList()
        {
            foreach (GameObject child in _contentHolder)
                Object.Destroy(child);
        }
    }
}