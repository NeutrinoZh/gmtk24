using GMTK.Services;
using UnityEngine;

namespace GMTK.MicroViruses
{
    public class CucumberVirus : VirusBehavior
    {
        private const float k_attackCooldown = 2;

        private const float k_speed = 0.1f;

        private Transform _transform;
        private Transform _target;
        private Rigidbody2D _rd;

        private Transform _bulletPrefab;

        private float _attackTime;

        private AudioSource _attackAudio;
        private Transform _bulletSpawnPoint;

        public CucumberVirus(Transform bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        public override void Init(Transform transform)
        {
            _transform = transform;
            _target = ServiceLocator.Instance.Get<PlayerStats>().Player;
            _rd = transform.GetComponent<Rigidbody2D>();

            _bulletSpawnPoint = transform.GetChild(0);
            _attackAudio = _bulletSpawnPoint.GetComponent<AudioSource>();
        }

        public override void OnEnable()
        {
            _attackTime = Time.time + k_attackCooldown;
        }

        public override void FixedUpdate()
        {
            if (_target == null)
                return;

            _rd.AddForce((_transform.position - _target.position).normalized * k_speed, ForceMode2D.Force);

            if (Time.time > _attackTime + k_attackCooldown)
            {
                _attackTime = Time.time + k_attackCooldown;

                var bullet = Object.Instantiate(_bulletPrefab, _bulletSpawnPoint);
                bullet.transform.position = _transform.GetChild(0).position;
                bullet.transform.right = (_target.position - _transform.position).normalized;

                _attackAudio.Play();
            }
        }
    }
}