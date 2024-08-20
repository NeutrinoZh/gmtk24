using GMTK.Services;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GMTK
{
    public class AudioMixerController : MonoBehaviour, IService
    {
        [SerializeField] private AudioClip _menuTrack;
        [SerializeField] private AudioClip _gameplayTrack;
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private AudioMixerGroup _incell;

        private AudioSource[] _audioSources;

        private bool _needToChange = false;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) => TryInitialize();

        public void TryInitialize()
        {

            ServiceLocator.Instance.TryRegister(this, out bool status);
            if (!status)
            {
                Destroy(gameObject);
                return;
            }

            _audioSources = GetComponents<AudioSource>();
            DontDestroyOnLoad(this);

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _needToChange = true;
                _audioSources[0].loop = false;
            }

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                _audioSources[1].Stop();
                _audioSources[1].Play();
                _audioSources[1].pitch = 0;
                _audioSources[0].Play();
            }
        }

        private void Update()
        {
            if (!_audioSources[0].isPlaying && _needToChange)
            {
                _needToChange = false;
                _audioSources[0].Stop();
                _audioSources[1].pitch = 1;
            }
        }

        public void SetIncell()
        {
            _audioSources[1].outputAudioMixerGroup = _incell;
        }

        public void SetMaster()
        {
            _audioSources[1].outputAudioMixerGroup = _master;
        }
    }
}