using GMTK.Services;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class Arrow : MonoBehaviour
    {
        private VirusManager _virusManager;

        private Transform _target;

        private void Start()
        {
            _virusManager = ServiceLocator.Instance.Get<VirusManager>();
            _virusManager.OnObjectRemoved += SetNewTarget;

            SetNewTarget();
        }

        private void Update()
        {
            SetArrowDirection();
        }

        private void SetNewTarget()
        {
            _target = _virusManager.FindNearToPoint(transform.position);
        }

        private void SetArrowDirection()
        {
            transform.rotation = Quaternion.Euler(0, 0, CalculateZRotation());
        }

        private float CalculateZRotation()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return angle;
        }
    }
}