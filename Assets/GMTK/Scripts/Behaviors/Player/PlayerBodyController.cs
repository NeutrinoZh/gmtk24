using GMTK.GameStates;
using GMTK.Services;
using GMTK.UI;
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
            _playerStats.OnUpgrade += OnUpgrade;
            _input = ServiceLocator.Instance.Get<InputController>();
            _body = GetComponent<DriftMovableObject>();
            _rd = GetComponent<Rigidbody2D>();
        }

        private void OnUpgrade(UpgradeType _type)
        {
            if (_type == UpgradeType.PLAYER_MOVE_SPEED)
                _body.SetSpeed(_body.GetSpeed() + 0.5f);
            if (_type == UpgradeType.PLAYER_MOBILITY)
                _body.SetAngularSpeed(_body.GetAngularSpeed() + 10);
        }

        private void FixedUpdate()
        {
            var inputValue = _input.Actions.PlayerBody.Move.ReadValue<Vector2>();
            var backMovement = inputValue.y < 0 ? 0.05f : 1f;

            _body.Move(inputValue.y * _playerStats.SpeedScale * backMovement);
            _body.Rotate(inputValue.x);

            _rd.velocity = Vector3.zero;
            _rd.angularVelocity = 0;
        }
    }
}