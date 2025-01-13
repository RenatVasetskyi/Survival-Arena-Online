using Business.Architecture.Services;
using Business.Architecture.Services.Interfaces;
using Business.Data;
using UnityEngine;
using Zenject;
using IFactory = Business.Architecture.Services.Interfaces.IFactory;

namespace Business.Architecture.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            BindGameSettings();
            BindCoroutineRunner();
            BindSceneLoader();
            BindAssetProvider();
            BindBaseFactory();
            BindUIFactory();
            BindSaveService();
            BindAudioService();
            BindGamePauser();
            BindEventService();
            BindPhotonConnectorService();
        }
        
        private void BindGamePauser()
        {
            Container
                .Bind<IGamePauser>()
                .To<GamePauser>()
                .AsSingle();
        }
        
        private void BindGameSettings()
        {
            Container
                .Bind<GameSettings>()
                .FromScriptableObject(_gameSettings)
                .AsSingle();
        }

        private void BindSaveService()
        {
            Container
                .Bind<ISaveService>()
                .To<SaveService>()
                .AsSingle();
        }
        
        private void BindAudioService()
        {
            Container
                .Bind<IAudioService>()
                .To<AudioService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindUIFactory()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();
        }
        
        private void BindBaseFactory()
        {
            Container
                .Bind<IFactory>()
                .To<BaseFactory>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container
                .BindInterfacesTo<ServiceInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }

		private void BindAssetProvider()
        {
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }
        
        private void BindEventService()
        {
            Container
                .Bind<IEventService>()
                .To<EventService>()
                .AsSingle();
        }
        
        private void BindPhotonConnectorService()
        {
            Container
                .Bind<IPhotonService>()
                .To<PhotonService>()
                .AsSingle();
        }
    }
}