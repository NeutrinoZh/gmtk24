using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerBodyController : MonoBehaviour
    {
        private InputController _input;
        private DriftMovableObject _body;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _input = ServiceLocator.Instance.Get<InputController>();
            _body = transform.GetComponent<DriftMovableObject>();
        }

        private void FixedUpdate()
        {
            var inputValue = _input.Actions.PlayerBody.Move.ReadValue<Vector2>();
            var backMovement = inputValue.y < 0 ? 0.05f : 1f;

            _body.Move(inputValue.y * _playerStats.Speed * backMovement);
            _body.Rotate(inputValue.x);
        }
    }
}