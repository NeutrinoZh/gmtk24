using GMTK.Services;
using UnityEngine;
using UnityEngine.Audio;

namespace GMTK
{
    public class AudioMixerController : MonoBehaviour, IService
    {
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private AudioMixerGroup _incell;

        private AudioSource _audioSource;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
            _audioSource = GetComponent<AudioSource>();
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