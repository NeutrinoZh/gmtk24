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
        [SerializeField] private int _destinationsCount = 3;
        [SerializeField] private float _distanceThreshold = 25f;
        private List<Vector2> _allDestinations;

        private void Start()
        {
            _allDestinations = new();
            InitList();
        }

        private void InitList()
        {
            for (int i = 0; i < _startBacteriaCount; ++i)
            {
                int randomBacteriaIndex = Random.Range(0, _prefabs.Count);
                BackgroundBacteria backgroundBacteria = Instantiate(_prefabs[randomBacteriaIndex], transform);
                backgroundBacteria.Init(GetRandomDestinationList());
            }
        }
        private List<Vector2> GetRandomDestinationList()
        {
            List<Vector2> destinationList = new List<Vector2>();

            for (int i = 0; i < _destinationsCount; i++)
            {
                while (true)
                {
                    var pos = new Vector2(Random.Range(_bounds.min.x, _bounds.max.x), Random.Range(_bounds.min.y, _bounds.max.y));

                    bool isCongruentWithOtherDestinations = true;

                    foreach (var vec in _allDestinations)
                    {
                        if ((pos - vec).sqrMagnitude <= _distanceThreshold)
                        {
                            isCongruentWithOtherDestinations = false;
                            break;
                        }
                    }

                    if (!isCongruentWithOtherDestinations) continue;
                    destinationList.Add(pos);
                    _allDestinations.Add(pos);
                    break;
                }
            }
            return destinationList;
        }

        void OnDrawGizmos()
        {
            if (_allDestinations == null) return;

            foreach (var item in _allDestinations)
            {
                // Gizmos.DrawSphere(item, 1f);
            }
        }
    }
}

