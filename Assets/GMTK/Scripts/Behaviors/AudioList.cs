using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class AudioList : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _clips;
        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Play(int i)
        {
            _source.clip = _clips[i];
            _source.Play();
        }
    }
}
