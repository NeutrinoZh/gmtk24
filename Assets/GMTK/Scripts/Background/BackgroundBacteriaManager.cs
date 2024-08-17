using GMTK.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK 
{
    public class BackgroundBacteriaManager : MonoBehaviour
    {
        [SerializeField] private List<BackgroundBacteria> _prefabs;
        [SerializeField] private int _startBacteriaCount;
        [SerializeField] private Bounds _bounds;
        private readonly List<BackgroundBacteria> _bacterias;

        private void Start() 
        {
            InitList();
        }

        private void InitList() 
        {
            for (int i = 0; i < _startBacteriaCount; ++i) 
            {
                int randomBacteriaIndex = Random.Range(0, _prefabs.Count);
                BackgroundBacteria backgroundBacteria = Instantiate(_prefabs[randomBacteriaIndex]);
                backgroundBacteria.Init(_bounds);
            }
        }
    }
}

