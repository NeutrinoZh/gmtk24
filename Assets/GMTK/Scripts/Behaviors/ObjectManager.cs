using GMTK.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK
{
    public class ObjectManager : MonoBehaviour, IService
    {
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

        public Transform FindNearToPoint(Vector3 position, out float sqrDistance)
        {
            Transform nearestObject = null;
            float minSqrDistance = Mathf.Infinity;

            foreach (Transform obj in _objects)
            {
                sqrDistance = (obj.position - position).sqrMagnitude;
                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    nearestObject = obj;
                }
            }

            sqrDistance = minSqrDistance;
            return nearestObject;
        }

        public void AddObject(Transform _object)
        {
            if (_objects.Contains(_object))
                return;

            _objects.Add(_object);
        }
    }
}