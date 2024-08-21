using GMTK.Services;
using UnityEngine;

namespace GMTK.MicroViruses
{
    public class OrangeVirus : VirusBehavior
    {
        private const float k_speed = 0.1f;

        private Transform _transform;
        private Transform _target;
        private Rigidbody2D _rd;

        public override void Init(Transform transform)
        {
            _transform = transform;
            _target = ServiceLocator.Instance.Get<PlayerStats>().Player;
            _rd = transform.GetComponent<Rigidbody2D>();
        }

        public override void FixedUpdate()
        {
            if (_target == null)
                return;

            _rd.AddForce((_target.position - _transform.position).normalized * k_speed, ForceMode2D.Force);
        }
    }
}