using UnityEngine;

namespace Business.Architecture.Services.Interfaces
{
    public interface IPhotonService
    {
        string ConnectionRoomName { get; }
        bool IsMasterClient { get; }
        public void Reconnect();
        public void JoinLobby();
        public void LeaveRoom();
        public void LeaveLobby();
        void JoinOrCreateRoom(string name, bool isVisible = true, bool isOpen = true);
        GameObject Instantiate(string loadedResourceName, Vector3 at, Quaternion rotation);
        void SetConnectionRoomName(string roomName);
    }
}