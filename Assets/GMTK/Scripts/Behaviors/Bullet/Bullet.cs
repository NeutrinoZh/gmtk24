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

        private void Start()
        {
            _bulletManager = ServiceLocator.Instance.Get<BulletManager>();
        }

        private void Update()
        {
            transform.position += Time.deltaTime * _speed * transform.right;
        }

        private void OnEnable()
        {
            StartCoroutine(BulletDestroyByDelay());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(_damage);
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