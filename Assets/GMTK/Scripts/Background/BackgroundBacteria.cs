using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK 
{
    public class BackgroundBacteria : MonoBehaviour
    {
        private ReadOnlyCollection<Vector2> _destinationsList;
        [SerializeField] private Vector2 _currentDestination;
        [SerializeField] private Vector2 _previousDestination;
        [SerializeField] private int _destinationsCount = 3;
        [SerializeField] private float _maxProgress = 5;
        private bool _isInitialized = false;
        private float _currentProgress;

        private void Update() 
        {
            if (!_isInitialized) return;

            transform.position = Vector2.Lerp(_previousDestination, _currentDestination, _currentProgress/_maxProgress);

            if (_currentProgress >= _maxProgress) 
            {
                SetNewDestination(_currentDestination);
                _currentProgress = 0;
            }
            else _currentProgress += Time.deltaTime;
        }

        void SetNewDestination(Vector2 previousDest) 
        {
            int index = _destinationsList.IndexOf(previousDest);
            _previousDestination = _destinationsList[index];
            _currentDestination = _destinationsList[index < _destinationsList.Count - 1 ? ++index : 0];
        }

        public void Init(Bounds bounds) 
        {
            _currentProgress = 0;

            var list = new List<Vector2>();

            for (int i = 0; i < _destinationsCount; i++)
            {
                list.Add(new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y)));
            }

            _destinationsList = list.AsReadOnly();

            transform.position.Set(list[0].x, list[0].y, 0);
            SetNewDestination(list[0]);
            _isInitialized = true;
        }
    }
}