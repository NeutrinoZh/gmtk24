using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Transform _playerPrefab;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            CreatePlayer();
            Destroy(gameObject);
        }

        private void CreatePlayer()
        {
            var instance = Instantiate(_playerPrefab);
            _playerStats.Player = instance;
        }


    }
}