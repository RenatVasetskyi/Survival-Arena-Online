namespace Business.Architecture.Services.Interfaces
{
    public interface IPhotonService
    {
        public void Reconnect();
        public void JoinLobby();
        public void LeaveRoom();
        public void LeaveLobby();
        void CreateRoom(string name, bool isVisible = true, bool isOpen = true);
    }
}