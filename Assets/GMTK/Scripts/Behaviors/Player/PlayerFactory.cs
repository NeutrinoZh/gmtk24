using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerFactory : MonoBehaviour
    {
        public const string k_bodyName = "Body";
        public const string k_turretName = "Turret";

        [SerializeField] private Transform _playerPrefab;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            CreatePlayer();
        }

        private void Start()
        {
            ServiceLocator.Instance.Get<CameraFollow>().Target = _playerStats.Player;
            Destroy(gameObject);
        }

        private void CreatePlayer()
        {
            var instance = Instantiate(_playerPrefab);
            _playerStats.Player = instance;
        }

    }
}