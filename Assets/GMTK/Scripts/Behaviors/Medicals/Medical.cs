using DG.Tweening;
using GMTK.Services;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class Medical : MonoBehaviour, IDamageable
    {
        private Animator _animator;
        private bool _isWhole = true;
        private int _score = 0;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_score == transform.childCount)
            {
                Debug.Log("Upgrade");
            }
        }

        public void Damage(int damage, Vector3 attackDirection)
        {
            if (!_isWhole)
                return;

            _isWhole = false;
            _animator.Play("Base Layer.Explosion");

            StartCoroutine(StartAnimationByDelay());
        }

        private IEnumerator StartAnimationByDelay()
        {
            yield return new WaitForSeconds(1f);
            _animator.enabled = false;
            foreach (Transform item in transform)
                item.GetComponent<Vitamin>().PlayAnimation(() => _score += 1);
        }
    }
}
