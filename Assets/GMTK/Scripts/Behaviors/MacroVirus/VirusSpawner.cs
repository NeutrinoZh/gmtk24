using GMTK.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK
{
    public class VirusManager : ObjectManager { };

    public class VirusSpawner : MonoBehaviour, IService
    {
        [SerializeField] private Transform _virusPrefab;
        [SerializeField] private float _virusCount;
        [SerializeField] private Bounds _spawnBounds;

        private VirusManager _virusManager;

        private void Awake()
        {
            _virusManager = transform.AddComponent<VirusManager>();
            ServiceLocator.Instance.Register(_virusManager);
            ServiceLocator.Instance.Register(this);

            for (int i = 0; i < _virusCount; ++i)
                SpawnVirus();
        }

        public void SpawnVirus()
        {
            var position = new Vector3(
                Random.Range(_spawnBounds.min.x, _spawnBounds.max.x),
                Random.Range(_spawnBounds.min.y, _spawnBounds.max.y),
                0
            );

            SpawnVirus(position);
        }

        public void SpawnVirus(Vector3 position)
        {
            var virus = Instantiate(_virusPrefab, transform);
            virus.SetPositionAndRotation(position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            _virusManager.AddObject(virus);
        }
    }
}