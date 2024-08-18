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

        private void Start()
        {
            _emissionModule = _particleSystem.emission;
            _bodyController = transform.GetComponent<DriftMovableObject>();

            _gamePlayState = ServiceLocator.Instance.Get<GamePlayState>();
            _gamePlayState.OnWorldChanged += ChangeParticle;
        }

        private void ChangeParticle(WorldState worldState)
        {
            if (worldState == WorldState.MICRO_WORLD) SetMicroWorldParticle();
            else SetMacroWorldParticle();
        }

        private void SetMicroWorldParticle()
        {
            var main = _particleSystem.main;
            main.startSpeed = 1f;
            main.startLifetime = 0.25f;
        }

        private void SetMacroWorldParticle()
        {
            var main = _particleSystem.main;
            main.startSpeed = 2f;
            main.startLifetime = 0.5f;
        }
    }
}