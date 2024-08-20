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
        private AudioSource _audioSource;

        private bool f = false;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) => TryInitialize();

        public void TryInitialize()
        {
            f = false;

            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            ServiceLocator.Instance.TryRegister(this, out bool status);
            if (!status)
            {
                Destroy(gameObject);
                return;
            }

            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (!_audioSource.isPlaying && !f)
            {
                f = true;
                _audioSource.PlayOneShot(SceneManager.GetActiveScene().buildIndex == 0 ? _menuTrack : _gameplayTrack);
            }
        }

        public void SetIncell()
        {
            _audioSource.outputAudioMixerGroup = _incell;
        }

        public void SetMaster()
        {
            _audioSource.outputAudioMixerGroup = _master;
        }
    }
}