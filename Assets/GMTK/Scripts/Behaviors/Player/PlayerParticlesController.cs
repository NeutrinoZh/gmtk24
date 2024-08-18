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

        }
    }
}
