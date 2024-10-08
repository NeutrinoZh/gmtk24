using GMTK.Services;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK
{
    public class VirusManager : ObjectManager { };

    public class VirusSpawner : MonoBehaviour, IService
    {
        [SerializeField] private Transform _virusPrefab;
        [SerializeField] private Transform _glistPrefab;

        [SerializeField] private float _virusCount;
        [SerializeField] private float _distanceFromCenter;

        private VirusManager _virusManager;
        private GameStatistics _gameStatistics;
        private PlayerStats _playerStats;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);

            _virusManager = transform.AddComponent<VirusManager>();
            ServiceLocator.Instance.Register(_virusManager);

            _gameStatistics = ServiceLocator.Instance.Get<GameStatistics>();
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
        }

        private void Update()
        {
            if (_virusManager.Pool.Count == 0 && _gameStatistics.SickCells() < 2)
            {
                for (int i = 0; i < _virusCount; ++i)
                    SpawnVirus();

                _virusCount *= 1.5f;
            }
        }

        public void SpawnVirus()
        {
            var rad = Random.Range(0, Mathf.PI * 2);
            var position = new Vector3(
                _distanceFromCenter * Mathf.Sin(rad),
                _distanceFromCenter * Mathf.Cos(rad),
               0
            );

            SpawnVirus(position);
        }

        public void SpawnVirus(Vector3 position)
        {
            var prefabs = new List<Transform>(){
                _virusPrefab
            };

            if (_playerStats.Time >= 80)
                prefabs.Add(_glistPrefab);

            var virus = Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform);
            virus.SetPositionAndRotation(position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            _virusManager.AddObject(virus);
        }
    }
}