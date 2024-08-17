using UnityEngine;
using DG.Tweening;

namespace GMTK.VirusBehaviors
{
    public class PenetrationIntoCell : IBehavior
    {
        private const float k_penetrationDuration = 0.5f;
        private const float k_scaleFactor = 0.5f;

        private Transform _target;
        private Transform _transform;

        public PenetrationIntoCell(Transform target)
        {
            _target = target;
        }

        void IBehavior.Init(Transform transform)
        {
            _transform = transform;
            transform.parent = _target;

            var originalScale = _transform.transform.localScale;
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_transform
                .DOScale(originalScale * k_scaleFactor, k_penetrationDuration)
                .SetEase(Ease.InOutQuad)
            ).OnComplete(OnAnimationEnd);
        }

        private void OnAnimationEnd()
        {
            Object.Destroy(_transform.gameObject);
        }

        void IBehavior.FixedUpdate()
        {

        }
    }
}