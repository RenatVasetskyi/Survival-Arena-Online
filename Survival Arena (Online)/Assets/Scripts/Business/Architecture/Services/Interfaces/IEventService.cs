using System;

namespace Business.Architecture.Services.Interfaces
{
    public interface IEventService
    {
        event Action OnPhotonInitialized;
        void SendPhotonInitialized();
    }
}