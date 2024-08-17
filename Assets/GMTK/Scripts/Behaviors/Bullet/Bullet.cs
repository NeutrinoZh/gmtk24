using GMTK.Services;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletLifetime;

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(1);
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