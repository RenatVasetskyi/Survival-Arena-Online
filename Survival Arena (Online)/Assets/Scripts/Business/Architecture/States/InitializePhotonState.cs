using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;

namespace Business.Architecture.States
{
    public class InitializePhotonState : IState
    {
        private const string InitializePhotonSceneName = "Initialize Photon";

        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEventService _eventService;

        public InitializePhotonState(IStateMachine stateMachine, ISceneLoader sceneLoader,
            IEventService eventService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _eventService = eventService;
        }

        public void Exit()
        {
            _eventService.OnPhotonInitialized -= LoadMenuScene;
        }

        public void Enter()
        {
            _eventService.OnPhotonInitialized += LoadMenuScene;
            _sceneLoader.Load(InitializePhotonSceneName);
        }

        private void LoadMenuScene()
        {
            _stateMachine.Enter<LoadMainMenuState>();
        }
    }
}