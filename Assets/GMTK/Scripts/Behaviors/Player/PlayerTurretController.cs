using GMTK.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK
{
    public class PlayerTurretController : MonoBehaviour
    {
        private InputController _input;
        private BulletManager _bulletManager;

        private Transform _turret;
        private Transform _bulletSpawnPoint;

        private void Start()
        {
            _input = ServiceLocator.Instance.Get<InputController>();
            _bulletManager = ServiceLocator.Instance.Get<BulletManager>();

            _turret = transform.Find(PlayerFactory.k_turretName);
            _bulletSpawnPoint = _turret.transform.GetChild(0);

            _input.Actions.PlayerTurret.Fire.performed += Fire;
        }

        private void Update()
        {
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
            _bulletManager.SpawnBullet(_bulletSpawnPoint.position, _turret.right);
        }
    }
}