using System.Collections.Generic;
using Business.Architecture.Services.Interfaces;
using Business.Audio;
using Business.Data;
using UnityEngine;

namespace Business.Architecture.Services
{
    public class AudioService : IAudioService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IFactory _factory;

        private readonly List<SfxData> _sfxDataList = new();
        private readonly List<MusicData> _musicDataList = new();

        private AudioSource _sfxAudioSource;
        private AudioSource _musicAudioSource;

        public AudioService(IAssetProvider assetProvider, IFactory factory)
        {
            _assetProvider = assetProvider;
            _factory = factory;
        }

        public void PlayMusic(MusicType musicType)
        {
            MusicData musicData = GetMusicData(musicType);
            _musicAudioSource.clip = musicData.Clip;
            _musicAudioSource.Play();
        }

        public void PlaySfx(SfxType sfxType)
        {
            SfxData sfxData = GetSfxData(sfxType);
            _sfxAudioSource.PlayOneShot(sfxData.Clip);
        }

        public void Initialize()
        {
            InitializeSfxDataList();
            InitializeMusicDataList();
            InitializeSfxAudioSource();
            InitializeMusicAudioSource();
        }

        public void StopMusic()
        {
            _musicAudioSource.Stop();
        }

        private MusicData GetMusicData(MusicType musicType)
        {
            return _musicDataList.Find(data => data.MusicType == musicType);
        }

        private SfxData GetSfxData(SfxType sfxType)
        {
            return _sfxDataList.Find(data => data.SfxType == sfxType);
        }
        
        private void InitializeSfxDataList()
        {
            SfxHolder sfxHolder = _assetProvider.Load<SfxHolder>(AssetPath.SfxHolder);

            _sfxDataList.AddRange(sfxHolder.SoundEffects);
        }

        private void InitializeMusicDataList()
        {
            MusicHolder musicHolder = _assetProvider.Load<MusicHolder>(AssetPath.MusicHolder);

            _musicDataList.AddRange(musicHolder.Musics);
        }
        
        private void InitializeSfxAudioSource()
        {
            _sfxAudioSource = _factory.CreateBaseWithContainer<AudioSource>(AssetPath.SfxAudioSource);
        }

        private void InitializeMusicAudioSource()
        {
            _musicAudioSource = _factory.CreateBaseWithContainer<AudioSource>(AssetPath.MusicAudioSource);
        }
    }
}