using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using UnityEngine.Device;
using IState = Architecture.States.Interfaces.IState;

namespace Architecture.States
{
    public class BootstrapState : IState
    {
        private const string BootSceneName = "Boot";

        private const int TargetFrameRate = 120;
        
        private readonly IStateMachine _stateMachine;
        private readonly IAudioService _audioService;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, IAudioService audioService, 
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _audioService = audioService;
            _sceneLoader = sceneLoader;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _sceneLoader.Load(BootSceneName, Initialize);
        }

        private void Initialize()
        {
            Application.targetFrameRate = TargetFrameRate; 
            
            _audioService.Initialize();
            
            _stateMachine.Enter<LoadMainMenuState>();
        }
    }
}