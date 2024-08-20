using GMTK;
using GMTK.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerFactory : MonoBehaviour
{
    [SerializeField] private AudioMixerController _src;

    void Start()
    {
        if (!ServiceLocator.Instance.Contains<AudioMixerController>()) {
            Instantiate(_src).TryInitialize();
        }
    }
}
