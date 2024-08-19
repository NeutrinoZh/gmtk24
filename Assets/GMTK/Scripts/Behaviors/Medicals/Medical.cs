using DG.Tweening;
using GMTK.Services;
using GMTK.UI;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class Medical : MonoBehaviour, IDamageable
    {
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private bool _isWhole = true;
        private int _score = 0;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Damage(int damage, Vector3 attackDirection)
        {
            if (!_isWhole)
                return;

            _isWhole = false;
            _animator.Play("Base Layer.Explosion");
            _boxCollider.enabled = false;

            StartCoroutine(StartAnimationByDelay());
        }

        private IEnumerator StartAnimationByDelay()
        {
            yield return new WaitForSeconds(1f);
            _animator.enabled = false;
            foreach (Transform item in transform)
                item.GetComponent<Vitamin>().PlayAnimation(VitaminCallback);
        }

        private void VitaminCallback()
        {
            _score += 1;
            if (_score == transform.childCount)
            {
                ServiceLocator.Instance.Get<PlayerStats>().AddExperience();
                Destroy(gameObject);
            }
        }
    }
}
