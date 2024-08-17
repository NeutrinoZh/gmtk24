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

            _body.Move(inputValue.y * _playerStats.Speed);
            _body.Rotate(inputValue.x);
        }
    }
}