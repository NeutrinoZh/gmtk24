using GMTK.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK
{
    public class ObjectManager : MonoBehaviour, IService
    {
        public Action OnObjectRemoved;

        private readonly List<Transform> _objects = new();

        public ReadOnlyCollection<Transform> Pool => _objects.AsReadOnly();

        public Transform FindNearToPoint(Vector3 position)
        {
            Transform nearestObject = null;
            float minSqrDistance = Mathf.Infinity;

            foreach (Transform obj in _objects)
            {
                float sqrDistance = (obj.position - position).sqrMagnitude;
                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    nearestObject = obj;
                }
            }

            return nearestObject;
        }

        public void AddObject(Transform _object)
        {
            if (_objects.Contains(_object))
                return;

            _objects.Add(_object);
        }

        public void RemoveObject(Transform _object)
        {
            if (!_objects.Contains(_object))
                return;

            _objects.Remove(_object);
            OnObjectRemoved?.Invoke();
        }
    }
}