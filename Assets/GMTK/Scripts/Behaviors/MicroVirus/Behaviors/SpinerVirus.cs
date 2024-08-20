using GMTK.Services;
using UnityEngine;

namespace GMTK.MicroViruses
{
    public class SpinerVirus : VirusBehavior
    {
        private const float k_impulse = 5f;
        private const float k_attackCooldown = 4f;

        private Transform _transform;
        private Transform _target;
        private Rigidbody2D _rd;

        private Animator _animator;
        private float _attackTime;
        private bool _animationPlaying;

        public override void Init(Transform transform)
        {
            _transform = transform;
            _target = ServiceLocator.Instance.Get<PlayerStats>().Player;
            _rd = transform.GetComponent<Rigidbody2D>();
            _animator = transform.GetComponent<Animator>();

        }
        public override void OnEnable()
        {
            _attackTime = Time.time + k_attackCooldown;
            _animationPlaying = false;
        }

        public override void FixedUpdate()
        {
            if (_target == null)
                return;

            if (Time.time > (_attackTime + k_attackCooldown - 1) && !_animationPlaying)
            {
                _animationPlaying = true;
                _animator.Play("Base Layer.Attack");
            }

            if (Time.time > _attackTime + k_attackCooldown)
            {
                _attackTime = Time.time + k_attackCooldown;
                _animationPlaying = false;

                _rd.AddForce((_target.position - _transform.position).normalized * k_impulse, ForceMode2D.Impulse);
            }
        }
    }
}