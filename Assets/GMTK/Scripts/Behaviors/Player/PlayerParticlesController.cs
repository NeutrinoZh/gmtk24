using DG.Tweening.Plugins;
using GMTK.GameStates;
using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class PlayerParticlesController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Vector2 _minMaxEmissionRate;

        private DriftMovableObject _bodyController;
        private ParticleSystem.EmissionModule _emissionModule;
        private GamePlayState _gamePlayState;

        private float _microWorldValue = 1f;

        private void Start()
        {
            _emissionModule = _particleSystem.emission;
            _bodyController = transform.GetComponent<DriftMovableObject>();

            _gamePlayState = ServiceLocator.Instance.Get<GamePlayState>();
            _gamePlayState.OnWorldChanged += ChangeParticle;
        }

        private void Update()
        {
            ChangeEmission();
        }

        private void ChangeParticle(WorldState worldState)
        {
            _microWorldValue = worldState == WorldState.MICRO_WORLD ? 3f : 1f;
        }
        private void ChangeEmission()
        {
            _emissionModule.rateOverTime = Mathf.Lerp(_minMaxEmissionRate.x, _minMaxEmissionRate.y,
                _bodyController.GetVelocity().magnitude / _bodyController.GetSpeed() * _microWorldValue);
        }
    }
}