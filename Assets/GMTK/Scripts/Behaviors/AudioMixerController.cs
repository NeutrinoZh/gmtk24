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
            if (ServiceLocator.Instance.Contains<AudioMixerController>())
            {
                Destroy(gameObject);
                return;
            }

            ServiceLocator.Instance.RegisterGlobal(this);

            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this);

            _audioSources = GetComponents<AudioSource>();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode _)
        {
            if (scene.buildIndex == 1)
            {
                _needToChange = true;
                _audioSources[0].loop = false;
            }

            if (scene.buildIndex == 0)
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