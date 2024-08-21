using GMTK.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace GMTK
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private VirusManager _virusManager;
        private VirusCellManager _virusCellManager;
        private Transform _target;
        private Sprite _arrowSprite;

        private bool _isActive = false;

        public Transform FindTargetPosition()
        {
            var value = _virusManager.FindNearToPoint(transform.position);
            if (value != null) return value;

            value = _virusCellManager.FindNearToPoint(transform.position);
            return value;
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
            _virusCellManager = ServiceLocator.Instance.Get<VirusCellManager>();
            _virusManager.OnObjectRemoved += SetNewTarget;
            _virusCellManager.OnObjectRemoved += SetNewTarget;

            _arrowSprite = _spriteRenderer.sprite;
        }

        private void Update()
        {
            if (!_isActive || _target == null) return;

            SetArrowDirection();
        }

        private void SetNewTarget()
        {
            _target = FindTargetPosition();
        }

        private void SetArrowDirection()
        {
            transform.right = (_target.position - transform.position).normalized;
        }
    }
}