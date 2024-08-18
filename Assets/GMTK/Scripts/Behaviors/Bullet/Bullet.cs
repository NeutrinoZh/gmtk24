using GMTK.GameStates;
using GMTK.Services;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletLifetime;
        [SerializeField] private int _damage;

        private BulletManager _bulletManager;
        private GamePlayState _gamePlayState;

        private void Update()
        {
            var velocity = Time.deltaTime * _speed * transform.right;
            if (_gamePlayState.State == WorldState.MICRO_WORLD)
                velocity *= 0.3f;
            transform.position += velocity;
        }

        private void OnEnable()
        {
            _bulletManager ??= ServiceLocator.Instance.Get<BulletManager>();
            _gamePlayState ??= ServiceLocator.Instance.Get<GamePlayState>();

            StartCoroutine(BulletDestroyByDelay());

            if (_gamePlayState.State == WorldState.MICRO_WORLD) transform.localScale = new(0.3f, 0.3f, 1);
            if (_gamePlayState.State == WorldState.MACRO_WORLD) transform.localScale = new(1f, 1f, 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(_damage, transform.right);
                _bulletManager.DestroyBullet(transform);
            }
        }

        private IEnumerator BulletDestroyByDelay()
        {
            yield return new WaitForSeconds(_bulletLifetime);
            _bulletManager.DestroyBullet(transform);
        }
    }
}