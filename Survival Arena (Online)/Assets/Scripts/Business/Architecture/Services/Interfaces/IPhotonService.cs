namespace Business.Architecture.Services.Interfaces
{
    public interface IPhotonService
    {
        public bool IsConnected { get; }
        public bool IsConnectedAndReady { get; }
        public bool InRoom { get; }
        public void Reconnect();
        public void JoinLobby();
        public void LeaveRoom();
        public void LeaveLobby();
        void AddCallbackTarget(object target);
        void RemoveCallbackTarget(object target);
    }
}