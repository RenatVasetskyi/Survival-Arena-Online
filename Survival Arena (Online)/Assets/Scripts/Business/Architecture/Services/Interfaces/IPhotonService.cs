using UnityEngine;

namespace Business.Architecture.Services.Interfaces
{
    public interface IPhotonService
    {
        public void Reconnect();
        public void JoinLobby();
        public void LeaveRoom();
        public void LeaveLobby();
        void CreateRoom(string name, bool isVisible = true, bool isOpen = true);
        GameObject Instantiate(string loadedResourceName, Vector3 at, Quaternion rotation);
        void AutomaticallySyncScene(bool automaticallySync);
    }
}