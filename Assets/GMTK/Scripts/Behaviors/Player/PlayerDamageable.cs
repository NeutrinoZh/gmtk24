using GMTK.Services;
using GMTK.UI;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private Material _damageMaterial;

        private bool _isAlive;
        private int _health;

        private DriftMovableObject _body;
        private Animator _animator;

        private Material _defaultMaterial;

        private void Start()
        {
            _defaultMaterial = GetComponentInChildren<SpriteRenderer>().material;
            _animator = GetComponentInChildren<Animator>();
            _body = GetComponent<DriftMovableObject>();
            _health = _maxHealth;
            _isAlive = true;
        }

        public void Damage(int damage, Vector3 attackDirection)
        {
            if (!_isAlive)
                return;

            _health -= damage;
            if (_health <= 0)
            {
                _isAlive = false;
                StartCoroutine(AnimationOnDied());
                Destroy(gameObject, 2f);
            }

            StartCoroutine(AnimationOnDamage(attackDirection));
        }

        private IEnumerator AnimationOnDied()
        {
            yield return new WaitForSeconds(0.2f);

            transform.Find("Turret").gameObject.SetActive(false);
            _animator.Play("Base Layer.Died");

            yield return new WaitForSeconds(1f);

            ServiceLocator.Instance.Get<HUD>().GameOverGroup(true);
        }

        private IEnumerator AnimationOnDamage(Vector3 attackDirection)
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.material = _damageMaterial;

            _body.Impulse(-attackDirection * 0.6f);

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.material = _defaultMaterial;
        }
    }
}