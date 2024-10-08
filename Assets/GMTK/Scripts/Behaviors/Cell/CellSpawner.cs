using GMTK.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK
{
    public class CellManager : ObjectManager { };
    public class VirusCellManager : ObjectManager { };

    public class CellSpawner : MonoBehaviour
    {
        public const int MAX_CHOICES_VALUE = 5000;

        [SerializeField] private Transform _cellPrefab;
        [SerializeField] private float _cellCount;
        [SerializeField, Tooltip("Squared distance threshold between cells")]
        private float _distanceThreshold = 10f;
        [SerializeField] private float _sqrSizeOfStartZone;

        [SerializeField] private Bounds _spawnBounds;

        private CellManager _cellManager;
        private VirusCellManager _virusCellManager;

        private void Awake()
        {
            _cellManager = transform.AddComponent<CellManager>();
            _virusCellManager = transform.AddComponent<VirusCellManager>();
            ServiceLocator.Instance.Register(_cellManager);
            ServiceLocator.Instance.Register(_virusCellManager);

            for (int i = 0; i < _cellCount; ++i)
                SpawnCell();

            var gameStatistics = ServiceLocator.Instance.Get<GameStatistics>();
            gameStatistics.SetLivingCells((int)_cellCount);

            _cellManager.OnObjectRemoved += () => gameStatistics.CellDied();
        }

        private void SpawnCell()
        {
            Vector3 position;
            int i = 0;

            while (true)
            {
                position = new Vector3(
                    Random.Range(_spawnBounds.min.x, _spawnBounds.max.x),
                    Random.Range(_spawnBounds.min.y, _spawnBounds.max.y),
                    0
                );

                if (position.sqrMagnitude < _sqrSizeOfStartZone)
                    continue;

                bool isCongruentWithOtherDestinations = true;

                foreach (var existingCell in _cellManager.Pool)
                {
                    if ((position - existingCell.position).sqrMagnitude <= _distanceThreshold)
                    {
                        isCongruentWithOtherDestinations = false;
                        break;
                    }
                }

                i++;

                if (i >= MAX_CHOICES_VALUE || isCongruentWithOtherDestinations) break;
                else if (!isCongruentWithOtherDestinations)
                    continue;
            }

            var cell = Instantiate(_cellPrefab, transform);
            cell.SetPositionAndRotation(position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            _cellManager.AddObject(cell);
        }
    }
}