using GMTK.Services;
using UnityEngine;

namespace GMTK.VirusBehaviors
{
    public class MoveToCell : IBehavior
    {
        private CellManager _cellManager;
        private DriftMovableObject _body;
        private Transform _target;
        private Transform _transform;

        void IBehavior.Init(Transform transform)
        {
            _transform = transform;
            _body = transform.GetComponent<DriftMovableObject>();
            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _target = _cellManager.FindNearToPoint(transform.position);
        }

        void IBehavior.FixedUpdate()
        {
            if (_target == null)
            {
                _target = _cellManager.FindNearToPoint(_transform.position);
                return;
            }

            var direction = _target.position - _transform.position;
            float product = _transform.right.x * direction.y - _transform.right.y * direction.x;

            _body.Rotate(Mathf.Sign(product));
            _body.Move(1f);
        }
    }
}