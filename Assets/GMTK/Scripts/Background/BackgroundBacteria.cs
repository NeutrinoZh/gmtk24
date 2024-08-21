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
        [SerializeField] private float _maxProgress = 5;
        private bool _isInitialized = false;
        private float _currentProgress;

        private void Update()
        {
            if (!_isInitialized) return;

            transform.position = Vector2.Lerp(_previousDestination, _currentDestination, _currentProgress / _maxProgress);
            transform.right = (_currentDestination - _previousDestination).normalized;

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

        public void Init(List<Vector2> destinationLists)
        {
            _currentProgress = 0;
            _destinationsList = destinationLists.AsReadOnly();
            transform.position.Set(_destinationsList[0].x, _destinationsList[0].y, 0);
            SetNewDestination(_destinationsList[0]);
            _isInitialized = true;
        }
    }
}