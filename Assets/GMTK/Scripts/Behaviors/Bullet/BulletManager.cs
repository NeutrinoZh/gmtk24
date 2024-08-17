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
        [SerializeField] private float _bulletLifetime;

        private IObjectPool<Transform> _bulletPool;


        private void Awake()
        {
            ServiceLocator.Instance.Register(this);

            _bulletPool = new ObjectPool<Transform>(
                () => Instantiate(_bulletPrefab, transform),
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                obj => Destroy(obj.gameObject),
                true, k_poolCapacity
            );
        }

        public void SpawnBullet(Vector3 position, Vector3 direction)
        {
            var obj = _bulletPool.Get();

            obj.transform.position = position;
            obj.transform.right = direction;

            StartCoroutine(BulletDestroyByDelay(obj));
        }

        private IEnumerator BulletDestroyByDelay(Transform bullet)
        {
            yield return new WaitForSeconds(_bulletLifetime);
            _bulletPool.Release(bullet);
        }
    }
}