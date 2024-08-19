using GMTK.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK
{
    public class PlayerTurretController : MonoBehaviour
    {
        [SerializeField] private float _baseReloadTime;
        [SerializeField] private float _scaleByLevelUpgrade;

        private float _lastFireTime;
        private float _reloadTime;

        private bool _isFire = false;

        private InputController _input;
        private BulletManager _bulletManager;
        private Transform _turret;
        private Transform _bulletSpawnPoint;
        private PlayerStats _playerStats;

        private void Start()
        {
            _input = ServiceLocator.Instance.Get<InputController>();
            _bulletManager = ServiceLocator.Instance.Get<BulletManager>();
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();

            _turret = transform.Find(PlayerFactory.k_turretName);
            _bulletSpawnPoint = _turret.transform.GetChild(0);

            _input.Actions.PlayerTurret.Fire.performed += ctx => _isFire = true;
            _input.Actions.PlayerTurret.Fire.canceled += ctx => _isFire = false;
        }

        private void Update()
        {
            if (Time.time > _lastFireTime + _reloadTime && _isFire)
            {
                _bulletManager.SpawnBullet(_bulletSpawnPoint.position, _turret.right);

                _lastFireTime = Time.time;
                _reloadTime = _baseReloadTime / (_playerStats.GetLevelUpgrade(UI.UpgradeType.PLAYER_ATTACK_SPEED) * _scaleByLevelUpgrade);
            }

            var screenPosition = (Vector3)_input.Actions.PlayerTurret.Pointer.ReadValue<Vector2>();
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            var direction = (worldPosition - transform.position).normalized;
            direction.z = 0;

            _turret.right = direction;

#if UNITY_EDITOR
            Debug.DrawLine(transform.position, worldPosition);
#endif
        }
    }
}