using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class CameraFollow : MonoBehaviour, IService
    {
        [SerializeField] private float _cameraSpeed;
        [field: SerializeField] public Vector3 Offset { get; set; } = Vector3.zero;
        public Transform Target { get; set; } = null;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position + Offset, _cameraSpeed * Time.deltaTime);
        }
    }
}