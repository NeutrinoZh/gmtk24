using System.Collections.Generic;
using UnityEngine;

namespace GMTK 
{
    public class PlayerParticlesController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Vector2 _minMaxEmissionRate;
        private DriftMovableObject _bodyController;
        private ParticleSystem.EmissionModule _emissionModule;

        private void Start() 
        {
            _emissionModule = _particleSystem.emission;
            _bodyController = transform.GetComponent<DriftMovableObject>();
            _bodyController.OnSpeedChanged += HandleSpeedChanged;
        }

        private void OnDestroy() 
        {
            _bodyController.OnSpeedChanged -= HandleSpeedChanged;
        }

        private void HandleSpeedChanged(float speed)
        {
            _emissionModule.rateOverTime = Mathf.Lerp(_minMaxEmissionRate.x, _minMaxEmissionRate.y, speed / GetMaxSpeed()); //.rateOverTime = speed;
        }

        private float GetMaxSpeed() => _bodyController.MaxSpeed;
    }
}
