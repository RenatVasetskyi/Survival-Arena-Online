using System.Collections;
using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Business.Architecture.Services
{
    public class PhotonService : IPhotonService, IConnectionCallbacks, ILobbyCallbacks
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IEventService _eventService;
        
        private bool _isReconnecting;
        
        public bool IsConnected => PhotonNetwork.IsConnectedAndReady;
        public bool IsConnectedAndReady => PhotonNetwork.IsConnectedAndReady;
        public bool InRoom => PhotonNetwork.InRoom;

        public PhotonService(ICoroutineRunner coroutineRunner, IEventService eventService)
        {
            _coroutineRunner = coroutineRunner;
            _eventService = eventService;
            AddCallbackTarget(this);
        }

        ~PhotonService()
        {
            RemoveCallbackTarget(this);
        }
        
        public void Reconnect()
        {
            if (_isReconnecting)
            {
                Debug.LogError("Photon reconnection is processing...");
                return;
            }

            _isReconnecting = true;
            _coroutineRunner.StartCoroutine(ReconnectCoroutine());
        }

        public void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void LeaveLobby()
        {
            PhotonNetwork.LeaveLobby();
        }

        public void AddCallbackTarget(object target)
        {
            PhotonNetwork.AddCallbackTarget(target);
        }
        
        public void RemoveCallbackTarget(object target)
        {
            PhotonNetwork.RemoveCallbackTarget(target);
        }
        
        public void OnConnectedToMaster()
        {
            _eventService.SendPhotonConnectedToMaster();
            _isReconnecting = false;
            Debug.Log("Photon connected to master.");
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _eventService.SendRoomListUpdated(roomList);
        }
        
        private IEnumerator ReconnectCoroutine()
        {
            if (InRoom)
                PhotonNetwork.LeaveRoom();
            
            PhotonNetwork.Disconnect();
            
            yield return new WaitUntil(() => !PhotonNetwork.IsConnected);
            
            Debug.Log("Photon connection...");
            PhotonNetwork.ConnectUsingSettings();
        }
        
        public void OnConnected() { }
        public void OnDisconnected(DisconnectCause cause) { }
        public void OnRegionListReceived(RegionHandler regionHandler) { }
        public void OnCustomAuthenticationResponse(Dictionary<string, object> data) { }
        public void OnCustomAuthenticationFailed(string debugMessage) { }
        public void OnJoinedLobby() { }
        public void OnLeftLobby() { }
        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
    }
}