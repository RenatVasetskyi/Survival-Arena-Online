using Business.Architecture.States;
using Business.Architecture.States.Interfaces;
using Photon.Pun;
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

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            
            _stateMachine.Enter<LoadGameState>();
        }
    }
}