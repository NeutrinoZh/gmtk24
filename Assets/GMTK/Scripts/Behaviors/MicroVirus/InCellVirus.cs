using System.Collections;
using UnityEngine;

namespace GMTK.MicroViruses
{
    public enum InCellVirusBehaviorType
    {
        ORANGE
    };

    public class InCellVirus : MonoBehaviour, IDamageable
    {

        private const float k_attackCooldown = 2f;

        [SerializeField] private InCellVirusBehaviorType _type;
        [SerializeField] private Material _damageMaterial;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;

        private int _health;
        private VirusBehavior _virusBehavior;
        private InCellVirusManager _virusManager;
        private Animator _animator;

        private float _attackTime;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = _maxHealth;

            switch (_type)
            {
                case InCellVirusBehaviorType.ORANGE:
                    _virusBehavior = new OrangeVirus();
                    break;
                default:
                    break;
            }
        }

        public void Init(InCellVirusManager virusManager)
        {
            _virusManager = virusManager;
        }

        private void Start()
        {
            _virusBehavior.Init(transform);
        }

        private void Update()
        {
            _virusBehavior.Update();
        }

        private void FixedUpdate()
        {
            _virusBehavior.FixedUpdate();
        }

        public void Damage(int damage, Vector3 attackDirection)
        {
            _health -= damage;
            if (_health <= 0)
            {
                StartCoroutine(AnimationOnDied());

                _virusManager.RemoveObject(transform);
                Destroy(gameObject, 1f);
            }

            StartCoroutine(AnimationOnDamage(attackDirection));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (Time.time < _attackTime + k_attackCooldown)
                return;

            if (other.gameObject.TryGetComponent(out PlayerDamageable damageable))
            {
                _attackTime = Time.time;
                damageable.Damage(_damage, transform.right);
            }
        }

        private IEnumerator AnimationOnDied()
        {
            yield return new WaitForSeconds(0.2f);
            _animator.Play("Base Layer.Died");
        }

        private IEnumerator AnimationOnDamage(Vector3 attackDirection)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var defaultMaterial = spriteRenderer.material;

            spriteRenderer.material = _damageMaterial;

            if (transform.TryGetComponent(out Rigidbody2D rd))
                rd.AddForce(attackDirection * 0.3f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.material = defaultMaterial;
        }
    }
}