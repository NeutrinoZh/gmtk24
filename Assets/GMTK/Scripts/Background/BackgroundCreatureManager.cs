using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class BackgroundCreatureManager : MonoBehaviour
    {
        public const int MAX_CHOICES_VALUE = 500;

        [SerializeField] private BackgroundCreature _prefab;
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
                BackgroundCreature backgroundBacteria = Instantiate(_prefab, transform);
                backgroundBacteria.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                backgroundBacteria.Init(GetRandomDestinationList());
            }
        }
        private List<Vector2> GetRandomDestinationList()
        {
            List<Vector2> destinationList = new List<Vector2>();

            for (int i = 0; i < _destinationsCount; i++)
            {
                int j = 0;
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

                    j++;

                    if (j >= MAX_CHOICES_VALUE || isCongruentWithOtherDestinations)
                    {
                        destinationList.Add(pos);
                        _allDestinations.Add(pos);
                        break;
                    }
                    else if (!isCongruentWithOtherDestinations)
                        continue;
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

