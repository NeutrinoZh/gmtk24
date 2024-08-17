using GMTK.GameStates;
using GMTK.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GMTK.UI
{
    public class AdviceDialog : MonoBehaviour, IService
    {
        [SerializeField] private float _distanceToActivate;

        private GamePlayState _gamePlayState;
        private CellManager _cellManager;
        private PlayerStats _playerStats;
        private InputController _input;

        private GameObject _canvas;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        private void Start()
        {
            _canvas = transform.GetChild(0).gameObject;
            _canvas.SetActive(false);

            _input = ServiceLocator.Instance.Get<InputController>();
            _input.Actions.PlayerPenetration.Penetration.performed += OnPlayerPenetration;

            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _gamePlayState = ServiceLocator.Instance.Get<GamePlayState>();
        }

        private void Update()
        {
            if (_gamePlayState.State != WorldState.MACRO_WORLD)
                return;

            var nearestCell = _cellManager.FindNearToPoint(_playerStats.Player.position, out float sqrDistance);
            if (sqrDistance < _distanceToActivate)
            {
                _canvas.transform.position = nearestCell.position;
                _canvas.SetActive(true);
            }
            else
                _canvas.SetActive(false);
        }

        private void OnPlayerPenetration(InputAction.CallbackContext context)
        {
            if (_canvas.activeSelf)
            {
                _canvas.SetActive(false);
                _gamePlayState.State = WorldState.MICRO_WORLD;
            }
            else
            {
                _gamePlayState.State = WorldState.MACRO_WORLD;
            }
        }
    }
}