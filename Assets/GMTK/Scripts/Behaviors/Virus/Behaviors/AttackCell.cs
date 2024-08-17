using UnityEngine;
using DG.Tweening;

namespace GMTK.VirusBehaviors
{
    public class AttackCell : IBehavior
    {
        private const float k_attackDuration = 0.5f;

        private Transform _target;
        private Transform _transform;

        public AttackCell(Transform target)
        {
            _target = target;
        }

        void IBehavior.Init(Transform transform)
        {
            _transform = transform;

            if (transform.TryGetComponent(out BoxCollider2D collider))
                collider.isTrigger = true;

            transform.parent = _target;

            var originalScale = _transform.transform.localScale;
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_transform
                .DOScale(originalScale * 0.5f, k_attackDuration)
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