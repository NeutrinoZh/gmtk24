using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace GMTK.MicroViruses
{
    public class InCellVirusManager : ObjectManager { };

    public class InCellViruses : MonoBehaviour
    {
        [SerializeField] private Transform _orangePrefab;
        [SerializeField] private Bounds _bounds;

        private int _countSpawnedViruses;

        private InCellVirusManager _virusManager;

        private void Awake()
        {
            _virusManager = gameObject.AddComponent<InCellVirusManager>();
        }

        public void SetVirusesCount(int _count)
        {
            for (int i = _countSpawnedViruses; i < _count; ++i)
                SpawnVirus();

            _countSpawnedViruses = _count;

            foreach (Transform virus in _virusManager.Pool)
                SetRandomPosition(virus);
        }

        private void SpawnVirus()
        {
            var inCellVirus = Instantiate(_orangePrefab, transform);
            inCellVirus.GetComponent<InCellVirus>().Init(_virusManager);
            _virusManager.AddObject(inCellVirus);
        }

        private void SetRandomPosition(Transform virus)
        {
            var position = new Vector3(
                Random.Range(_bounds.min.x, _bounds.max.x),
                Random.Range(_bounds.min.y, _bounds.max.y),
                0
            );

            virus.localPosition = position;
        }
    }
}