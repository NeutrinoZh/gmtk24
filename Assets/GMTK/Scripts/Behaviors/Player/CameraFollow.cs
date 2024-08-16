using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _cameraSpeed;
        [SerializeField] private Vector3 _offset;

        private PlayerStats _playerStats;

        private void Awake()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerStats.Player.position + _offset, _cameraSpeed * Time.deltaTime);
        }
    }
}