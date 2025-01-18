using Business.Architecture.States;
using Business.Architecture.States.Interfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Test__Temporarily_
{
    public class EnterRoomButton : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button _button;
        
        [Inject] private IStateMachine _stateMachine; 

        private void OnEnable()
        {
            _button.onClick.AddListener(EnterRoom);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(EnterRoom);
        }

        private void EnterRoom()
        {
            PhotonNetwork.JoinRoom("Try");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            
            _stateMachine.Enter<LoadGameState>();
        }
    }
}