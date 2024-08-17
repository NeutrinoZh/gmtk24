using GMTK.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK
{
    public class CellManager : ObjectManager { };

    public class CellSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _cellPrefab;
        [SerializeField] private float _cellCount;
        [SerializeField] private Bounds _spawnBounds;


        private void Awake()
        {
            var manager = transform.AddComponent<CellManager>();
            ServiceLocator.Instance.Register(manager);

            for (int i = 0; i < _cellCount; ++i)
                SpawnCell();
        }

        private void SpawnCell()
        {
            var position = new Vector3(
                Random.Range(_spawnBounds.min.x, _spawnBounds.max.x),
                Random.Range(_spawnBounds.min.y, _spawnBounds.max.y),
                0
            );

            var cell = Instantiate(_cellPrefab, transform);
            cell.SetPositionAndRotation(position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}