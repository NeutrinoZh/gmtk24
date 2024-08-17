using GMTK.Services;
using UnityEngine;

namespace GMTK
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private VirusManager _virusManager;
        private Transform _target;
        private Sprite _arrowSprite;

        private bool _isActive = false;

        public Transform FindTargetPosition()
        {
            return _virusManager.FindNearToPoint(transform.position);
        }

        public void SetArrowState(bool state)
        {
            _isActive = state;
            if (_isActive) SetNewTarget();

            _spriteRenderer.sprite = _isActive ? _arrowSprite : null;
        }

        private void Start()
        {
            _virusManager = ServiceLocator.Instance.Get<VirusManager>();
            _virusManager.OnObjectRemoved += SetNewTarget;

            _arrowSprite = _spriteRenderer.sprite;
        }

        private void Update()
        {
            if (!_isActive) return;

            SetArrowDirection();
        }

        private void SetNewTarget()
        {
            _target = FindTargetPosition();
        }

        private void SetArrowDirection()
        {
            transform.rotation = Quaternion.Euler(0, 0, CalculateZRotation());
        }

        private float CalculateZRotation()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return angle;
        }
    }
}