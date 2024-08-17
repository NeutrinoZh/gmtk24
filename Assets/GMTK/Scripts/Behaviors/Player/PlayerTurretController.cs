using GMTK.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK
{
    public class PlayerTurretController : MonoBehaviour
    {
        public bool AbleToShoot => _currentReloadTime <= 0;

        [SerializeField] private float _reloadTime = 0.625f;
        private float _currentReloadTime;

        private InputController _input;
        private BulletManager _bulletManager;
        private Transform _turret;
        private Transform _bulletSpawnPoint;

        private void Start()
        {
            _currentReloadTime = 0;

            _input = ServiceLocator.Instance.Get<InputController>();
            _bulletManager = ServiceLocator.Instance.Get<BulletManager>();

            _turret = transform.Find(PlayerFactory.k_turretName);
            _bulletSpawnPoint = _turret.transform.GetChild(0);

            _input.Actions.PlayerTurret.Fire.performed += Fire;
        }

        private void Update()
        {
            if (!AbleToShoot) 
            {
                _currentReloadTime -= Time.deltaTime;
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

        private void Fire(InputAction.CallbackContext ctx)
        {
            if (!AbleToShoot) {
                // Debug.LogWarning($"Unable to shoot while cooldown is active!");
                return;
            }

            _bulletManager.SpawnBullet(_bulletSpawnPoint.position, _turret.right);
            _currentReloadTime = _reloadTime;
            // Debug.Log($"Bullet was spawned! Shoot!");
        }
    }
}