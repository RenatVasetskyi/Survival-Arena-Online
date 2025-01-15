using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Business.Architecture.States;
using Business.Architecture.States.Interfaces;
using Business.UI.RoomList;
using Business.UI.RoomList.Interfaces;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace Mono.UI.Menu.Rooms
{
    public class RoomList : MonoBehaviour
    {
        [SerializeField] private RoomListView _view;

        private IPhotonService _photonService;
        private IEventService _eventService;
        private IStateMachine _stateMachine;
        private IRoomListPresenter _presenter;

        [Inject]
        public void Inject(IUIFactory uiFactory, IPhotonService photonService,
            IEventService eventService, IStateMachine stateMachine)
        {
            _photonService = photonService;
            _eventService = eventService;
            _stateMachine = stateMachine; 
            _presenter = new RoomListPresenter(new RoomListModel(photonService), _view);
        }

        private void OnEnable()
        {
            _eventService.OnRoomListUpdated += OnRoomListUpdate;
            _eventService.OnJoinedRoom += EnterGameState;
            _view.CreateRoomButton.onClick.AddListener(CreateRoom);
            _photonService.JoinLobby();
        }

        private void OnDisable()
        {
            _eventService.OnRoomListUpdated -= OnRoomListUpdate;
            _eventService.OnJoinedRoom -= EnterGameState;
            _view.CreateRoomButton.onClick.RemoveListener(CreateRoom);
        }

        private void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _presenter.UpdateRoomList(roomList);
        }

        private void CreateRoom()
        {
            _presenter.CreateRoom("Try");
        }

        private void EnterGameState()
        {
            _stateMachine.Enter<LoadGameState>();
        }
    }
}