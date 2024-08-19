using GMTK.Services;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK
{
    public class MedicalManager : ObjectManager { };
    public class MedicalSpawner : MonoBehaviour, IService
    {
        [SerializeField] private Transform _medicalPrefab;
        [SerializeField] private Bounds _spawnBounds;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _numToSpawn;

        private MedicalManager _medicalManager;

        private void Start()
        {
            _medicalManager = transform.AddComponent<MedicalManager>();
            ServiceLocator.Instance.Register(_medicalManager);

            StartCoroutine(SpawnMedicalInterval());
        }

        private IEnumerator SpawnMedicalInterval()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);

                for (int i = 0; i < _numToSpawn; i++)
                    SpawnMedical();
            }
        }

        private void SpawnMedical()
        {
            var position = new Vector3(
               Random.Range(_spawnBounds.min.x, _spawnBounds.max.x),
               Random.Range(_spawnBounds.min.y, _spawnBounds.max.y),
               0
           );

            var virus = Instantiate(_medicalPrefab, transform);
            virus.SetPositionAndRotation(position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            _medicalManager.AddObject(virus);
        }
    }
}