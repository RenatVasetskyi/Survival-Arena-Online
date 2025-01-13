using Business.Architecture.Services.Interfaces;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Mono
{
    public class PhotonInitializer : MonoBehaviourPunCallbacks
    {
        private IEventService _eventService;
        
        [Inject]
        public void Inject(IEventService eventService)
        {
            _eventService = eventService;
        }
        
        private void Awake()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            _eventService.SendPhotonInitialized();
            Debug.Log("Connected to Master");
        }
    }
}