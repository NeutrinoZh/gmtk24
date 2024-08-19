using GMTK.Services;
using GMTK.VirusBehaviors;
using System.Collections;
using UnityEngine;

namespace GMTK
{
    public class VirusController : MonoBehaviour, IDamageable
    {
        [SerializeField] public float _delayBeforePenetration;
        [SerializeField] private Material _damageMaterial;
        [SerializeField] private int _maxHealth;

        private CellManager _cellManager;
        private VirusManager _virusManager;

        private DriftMovableObject _body;
        private Transform _target;
        private Animator _animator;
        private PlayerStats _playerStats;
        private BoxCollider2D _boxCollider;

        private IBehavior _behavior;

        private int _health;

        private AudioSource[] _audioSources;

        private void Start()
        {
            _health = _maxHealth;

            _animator = GetComponentInChildren<Animator>();
            _body = GetComponent<DriftMovableObject>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _target = _cellManager.FindNearToPoint(transform.position);

            _virusManager = ServiceLocator.Instance.Get<VirusManager>();

            _audioSources = GetComponents<AudioSource>();

            ChangeBehavior(new MoveToCell());
        }

        private void Update()
        {
            transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void FixedUpdate()
        {
            if (_behavior is AttackCell attackBehavior && !attackBehavior.Target)
            {
                ChangeBehavior(new MoveToCell());
                return;
            }

            _behavior.FixedUpdate();
        }

        void IDamageable.Damage(int damage, Vector3 attackDirection)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _boxCollider.enabled = false;
                StartCoroutine(AnimationOnDied());

                _playerStats.AddExperience();

                _virusManager.RemoveObject(transform);
                Destroy(gameObject, 1);
            }

            StartCoroutine(AnimationOnDamage(attackDirection));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out CellController cell) && cell.IsAlive)
            {
                ChangeBehavior(new AttackCell(cell.transform));
                StartCoroutine(StartPenetrationAfterDelay(cell.transform));
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
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            var defaultMaterial = spriteRenderer.material;

            spriteRenderer.material = _damageMaterial;

            _body.Impulse(attackDirection);

            _audioSources[0].Play();

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.material = defaultMaterial;
        }

        private IEnumerator StartPenetrationAfterDelay(Transform cell)
        {
            yield return new WaitForSeconds(_delayBeforePenetration);

            if (cell && cell.GetComponent<CellController>().IsAlive)
                ChangeBehavior(new PenetrationIntoCell(cell));
        }

        private void ChangeBehavior(IBehavior behavior)
        {
            _behavior = behavior;
            _behavior.Init(transform);
        }
    }
}