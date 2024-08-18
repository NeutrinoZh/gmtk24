using GMTK.GameStates;
using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerBodyController : MonoBehaviour
    {
        private InputController _input;

        private DriftMovableObject _body;
        private Rigidbody2D _rd;

        private PlayerStats _playerStats;
        private GamePlayState _playState;

        private void Awake()
        {
            _playState = ServiceLocator.Instance.Get<GamePlayState>();
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _input = ServiceLocator.Instance.Get<InputController>();
            _body = GetComponent<DriftMovableObject>();
            _rd = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var inputValue = _input.Actions.PlayerBody.Move.ReadValue<Vector2>();

            _body.Move(inputValue.y * _playerStats.Speed);
            _body.Rotate(inputValue.x);

            if (_playState.State == WorldState.MICRO_WORLD)
            {
                _rd.velocity = Vector3.zero;
                _rd.angularVelocity = 0;
            }
        }
    }
}