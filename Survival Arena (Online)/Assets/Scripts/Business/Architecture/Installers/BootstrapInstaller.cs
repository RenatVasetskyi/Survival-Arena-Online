using Business.Architecture.States;
using Business.Architecture.States.Interfaces;
using Zenject;
using StateMachine = Business.Architecture.States.StateMachine;

namespace Business.Architecture.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInterfaces();
            BindStateMachine();
            BindStates();
            AddStatesToStateMachine();
        }

        public void Initialize()
        {
            Container
                .Resolve<IStateMachine>()
                .Enter<BootstrapState>();
        }

        private void BindStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadMainMenuState>().AsSingle();
            Container.Bind<LoadGameState>().AsSingle();
        }

        private void AddStatesToStateMachine()
        {
            IStateMachine stateMachine = Container.Resolve<IStateMachine>();

            stateMachine.States.Add(typeof(BootstrapState), Container.Resolve<BootstrapState>());
            stateMachine.States.Add(typeof(LoadMainMenuState), Container.Resolve<LoadMainMenuState>());
            stateMachine.States.Add(typeof(LoadGameState), Container.Resolve<LoadGameState>());
        }

        private void BindStateMachine()
        {
            Container
                .Bind<IStateMachine>()
                .To<StateMachine>()
                .AsSingle();
        }

        private void BindInterfaces()
        {
            Container
                .BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}