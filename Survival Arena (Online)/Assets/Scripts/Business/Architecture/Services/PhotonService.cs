using System.Collections;
using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Business.Architecture.Services
{
    public class PhotonService : IPhotonService, IConnectionCallbacks, ILobbyCallbacks, IInRoomCallbacks
    {
        private const int MaxPlayers = 2;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IEventService _eventService;

        private bool _isReconnecting;

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
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();   
        }

        public void LeaveLobby()
        {
            if (PhotonNetwork.InLobby) 
                PhotonNetwork.LeaveLobby();
        }
        
        public void CreateRoom(string name, bool isVisible = true, bool isOpen = true)
        {
            PhotonNetwork.CreateRoom(name, new RoomOptions { IsVisible = isVisible, 
                IsOpen = isOpen, MaxPlayers = MaxPlayers }, TypedLobby.Default);
        }

        public void OnConnectedToMaster()
        {
            _isReconnecting = false;
            _eventService.SendPhotonConnectedToMaster();
            Debug.Log("Photon connected to master.");
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _eventService.SendRoomListUpdated(roomList);
            Debug.Log($"Photon room list updated, room count: {roomList.Count}");
        }
        
        public void OnJoinedLobby()
        {
            _eventService.SendJoinedLobby();
            Debug.Log("Player joined lobby.");
        }

        public void OnLeftLobby()
        {
            _eventService.SendLeftLobby();
            Debug.Log("Player left lobby.");
        }
        
        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Player entered room: " + newPlayer.NickName);
        }
        
        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left room: " + otherPlayer.NickName);
        }

        private void AddCallbackTarget(object target)
        {
            PhotonNetwork.AddCallbackTarget(target);
        }
        
        private void RemoveCallbackTarget(object target)
        {
            PhotonNetwork.RemoveCallbackTarget(target);
        }
        
        private IEnumerator ReconnectCoroutine()
        {
            if (PhotonNetwork.InRoom)
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
        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) { }
        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) { }
        public void OnMasterClientSwitched(Player newMasterClient) { }
    }
}