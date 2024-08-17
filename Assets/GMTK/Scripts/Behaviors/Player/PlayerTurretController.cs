using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerTurretController : MonoBehaviour
    {
        private InputController _input;
        private Transform _turret;

        private void Awake()
        {
            _input = ServiceLocator.Instance.Get<InputController>();
            _turret = transform.Find(PlayerFactory.k_turretName);
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
    }
}