using GMTK.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK
{
    public class CellManager : ObjectManager { };

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

        private void Awake()
        {
            _cellManager = transform.AddComponent<CellManager>();
            ServiceLocator.Instance.Register(_cellManager);

            for (int i = 0; i < _cellCount; ++i)
                SpawnCell();
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