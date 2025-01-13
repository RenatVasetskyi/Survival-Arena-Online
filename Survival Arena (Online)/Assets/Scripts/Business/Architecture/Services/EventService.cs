using System;
using Business.Architecture.Services.Interfaces;

namespace Business.Architecture.Services
{
    public class EventService : IEventService
    {
        public event Action OnPhotonInitialized;
        
        public void SendPhotonInitialized()
        {
            OnPhotonInitialized?.Invoke();  
        }
    }
}