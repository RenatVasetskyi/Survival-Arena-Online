using System.Collections;
using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Business.Architecture.Services
{
    public class PhotonService : IPhotonService, IConnectionCallbacks, ILobbyCallbacks, IInRoomCallbacks, IMatchmakingCallbacks
    {
        private const int MaxPlayers = 3;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IEventService _eventService;

        private bool _isReconnecting;
        
        public string ConnectionRoomName { get; private set; }
        public bool IsMasterClient => PhotonNetwork.IsMasterClient;

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
                Debug.LogError("Photon reconnection is already processing...");
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
        
        public void JoinOrCreateRoom(string name, bool isVisible = true, bool isOpen = true)
        {
            PhotonNetwork.JoinOrCreateRoom(name, new RoomOptions { IsVisible = isVisible, 
                IsOpen = isOpen, MaxPlayers = MaxPlayers }, null );
        }

        public GameObject Instantiate(string loadedResourceName, Vector3 at, Quaternion rotation)
        {
            return PhotonNetwork.Instantiate(loadedResourceName, at, rotation);
        }

        public void SetConnectionRoomName(string roomName)
        {
            ConnectionRoomName = roomName;
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
        
        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Player entered room: " + newPlayer.NickName);
        }
        
        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left room: " + otherPlayer.NickName);
        }
        
        public void OnJoinedRoom()
        {
            _eventService.SendJoinedRoom();
        }
        
        public void OnJoinedLobby()
        {
            _eventService.SendJoinedLobby();
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
        public void OnFriendListUpdate(List<FriendInfo> friendList) { }
        public void OnCreateRoomFailed(short returnCode, string message) { }
        public void OnJoinRoomFailed(short returnCode, string message) { }
        public void OnJoinRandomFailed(short returnCode, string message) { }
        public void OnCreatedRoom() { }
        public void OnLeftLobby() { }
        public void OnLeftRoom() { }
    }
}