using UnityEngine;

namespace GMTK
{
    public class VirusBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletLifetime;
        [SerializeField] private int _damage;

        private void Start()
        {
            Destroy(gameObject, _bulletLifetime);
        }

        private void Update()
        {
            var velocity = Time.deltaTime * _speed * transform.right;
            transform.position += velocity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerDamageable damageable))
            {
                damageable.Damage(_damage, transform.right);
                Destroy(gameObject);
            }
        }
    }
}