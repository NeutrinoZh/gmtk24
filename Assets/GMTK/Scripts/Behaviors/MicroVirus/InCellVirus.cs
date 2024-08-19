using GMTK.Services;
using System.Collections;
using UnityEngine;

namespace GMTK.MicroViruses
{
    public enum InCellVirusBehaviorType
    {
        ORANGE,
        CUCUMBER
    };

    public class InCellVirus : MonoBehaviour, IDamageable
    {

        private const float k_attackCooldown = 2f;

        [SerializeField] private Transform _cucumberBulletPrefab;
        [SerializeField] private InCellVirusBehaviorType _type;
        [SerializeField] private Material _damageMaterial;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;

        private int _health;
        private VirusBehavior _virusBehavior;
        private InCellVirusManager _virusManager;
        private Animator _animator;
        private PlayerStats _playerStats;
        private AudioSource[] _audioSources;
        private BoxCollider2D _boxCollider;

        private float _attackTime;


        private void Awake()
        {
            _audioSources = GetComponents<AudioSource>();
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _health = _maxHealth;

            switch (_type)
            {
                case InCellVirusBehaviorType.ORANGE:
                    _virusBehavior = new OrangeVirus();
                    break;
                case InCellVirusBehaviorType.CUCUMBER:
                    _virusBehavior = new CucumberVirus(_cucumberBulletPrefab);
                    break;
                default:
                    break;
            }
        }

        public void Init(InCellVirusManager virusManager)
        {
            _virusManager = virusManager;
        }

        private void OnEnable()
        {
            _virusBehavior.OnEnable();
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
                _boxCollider.enabled = false;
                StartCoroutine(AnimationOnDied());

                _playerStats.AddExperience();
                _virusManager.RemoveObject(transform);
                Destroy(gameObject, 1f);
            }

            StartCoroutine(AnimationOnDamage(attackDirection));
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (Time.time < _attackTime + k_attackCooldown)
                return;

            if (other.gameObject.TryGetComponent(out PlayerDamageable damageable))
            {
                _attackTime = Time.time;
                damageable.Damage(_damage, (damageable.transform.position - transform.position).normalized);
            }
        }

        private IEnumerator AnimationOnDied()
        {
            yield return new WaitForSeconds(0.2f);
            _animator.Play("Base Layer.Died");
            _audioSources[1].Play();
        }

        private IEnumerator AnimationOnDamage(Vector3 attackDirection)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var defaultMaterial = spriteRenderer.material;

            spriteRenderer.material = _damageMaterial;

            if (transform.TryGetComponent(out Rigidbody2D rd))
                rd.AddForce(attackDirection * 0.8f, ForceMode2D.Impulse);

            _audioSources[0].Play();

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.material = defaultMaterial;
        }
    }
}