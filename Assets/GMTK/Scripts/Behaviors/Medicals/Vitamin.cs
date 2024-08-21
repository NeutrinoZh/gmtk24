using GMTK.Services;
using System;
using UnityEngine;

namespace GMTK
{
    public class Vitamin : MonoBehaviour
    {
        [SerializeField] private float _distanceToEnd;
        [SerializeField] private float _vitaminSpeed;

        private PlayerStats _playerStats;

        private Action _finishCallback;

        private bool _isPlaying = false;


        private void Start()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
        }

        private void Update()
        {
            if (!_isPlaying)
                return;

            transform.position = Vector3.Lerp(
                transform.position,
                _playerStats.Player.position,
                _vitaminSpeed * Time.deltaTime
            );

            if ((_playerStats.Player.position - transform.position).sqrMagnitude < _distanceToEnd)
            {
                _finishCallback?.Invoke();
                gameObject.SetActive(false);
            }
        }

        public void PlayAnimation(Action finishCallback)
        {
            _finishCallback = finishCallback;
            _isPlaying = true;
        }
    }
}