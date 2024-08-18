using UnityEngine;

namespace GMTK.VirusBehaviors
{
    public class AttackCell : IBehavior
    {
        private Transform _target;
        private Transform _transform;
        private DriftMovableObject _body;

        public AttackCell(Transform target)
        {
            _target = target;
        }

        void IBehavior.Init(Transform transform)
        {
            _transform = transform;
            _body = transform.GetComponent<DriftMovableObject>();
        }

        void IBehavior.FixedUpdate()
        {
            var direction = _target.position - _transform.position;
            float product = _transform.right.x * direction.y - _transform.right.y * direction.x;

            _body.Rotate(Mathf.Sign(product));
            _body.Move(0.05f);
        }
    }
}