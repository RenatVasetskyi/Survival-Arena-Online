using Business.Architecture.Services.Interfaces;
using Business.Architecture.States.Interfaces;
using UnityEngine.Device;

namespace Business.Architecture.States
{
    using IState = Interfaces.IState;

    public class BootstrapState : IState
    {
        private const string BootstrapSceneName = "Bootstrap";

        private const int TargetFrameRate = 120;
        
        private readonly IStateMachine _stateMachine;
        private readonly IAudioService _audioService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(IStateMachine stateMachine, IAudioService audioService, 
            ISceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _sceneLoader.Load(BootstrapSceneName, Initialize);
        }

        private void Initialize()
        {
            _uiFactory.CreateLoadingCurtain();
            
            Application.targetFrameRate = TargetFrameRate; 
            _audioService.Initialize();
            _stateMachine.Enter<InitializePhotonState>();
        }
    }
}