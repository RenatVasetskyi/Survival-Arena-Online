using Business.UI.RoomList.Interfaces;
using TMPro;
using UnityEngine;

namespace Mono.UI
{
    public class RoomButton : MonoBehaviour, IRoomButton
    {
        [SerializeField] private TextMeshProUGUI _roomName;
        [SerializeField] private TextMeshProUGUI _playerCount;
        
        public void Initialize(string roomName, string playerCount, string maxPlayerCount)
        {
            _roomName.text = roomName;
            _playerCount.text = $"{playerCount}/{maxPlayerCount}";
        }
    }
}