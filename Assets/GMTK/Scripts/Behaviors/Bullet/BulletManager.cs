using GMTK.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK
{
    public class BulletManager : MonoBehaviour, IService
    {
        private const int k_poolCapacity = 50;

        [SerializeField] private Transform _bulletPrefab;

        private IObjectPool<Transform> _bulletPool;

        public void SpawnBullet(Vector3 position, Vector3 direction)
        {
            var obj = _bulletPool.Get();

            obj.transform.position = position;
            obj.transform.right = direction;
        }

        public void DestroyBullet(Transform bullet)
        {
            _bulletPool.Release(bullet);
        }

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);

            _bulletPool = new ObjectPool<Transform>(
                () => Instantiate(_bulletPrefab, transform),
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                obj => Destroy(obj.gameObject),
                false, k_poolCapacity
            );
        }
    }
}