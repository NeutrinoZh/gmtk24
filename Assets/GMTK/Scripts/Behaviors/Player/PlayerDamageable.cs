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
        private PlayerStats _playerStats;
        private AudioList _audioList;

        private Material _defaultMaterial;

        public int Health => _health;
        public int MaxHealth => _maxHealth;

        public int Armor { get; set; } = 0;

        private void Start()
        {
            _audioList = GetComponent<AudioList>();

            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _playerStats.OnUpgrade += OnUpgrade;

            _defaultMaterial = GetComponentInChildren<SpriteRenderer>().material;
            _animator = GetComponentInChildren<Animator>();
            _body = GetComponent<DriftMovableObject>();
            _health = 3;
            _isAlive = true;
        }

        private void OnDestroy()
        {
            _playerStats.OnUpgrade -= OnUpgrade;
        }

        public void OnUpgrade(UpgradeType _upgrade)
        {
            if (_playerStats.Level % 2 == 0)
                _health += 1;

            if (_upgrade == UpgradeType.PLAYER_HP && _health < _maxHealth)
                _health += 1;
            if (_upgrade == UpgradeType.PLAYER_ARMOR && Armor < 8)
                Armor += 1;
        }

        public void Damage(int damage, Vector3 attackDirection)
        {
            if (!_isAlive)
                return;

            float chanceIgnore = Armor / 6.0f;
            if (Random.Range(0.1f, 1.0f) < chanceIgnore)
                Armor -= 1;
            else
                _health -= damage;

            if (_health <= 0)
            {
                _isAlive = false;
                StartCoroutine(AnimationOnDied());
                Destroy(gameObject, 2f);
            }

            StartCoroutine(AnimationOnDamage(attackDirection));
            _audioList.Play(2);
        }

        private IEnumerator AnimationOnDied()
        {
            yield return new WaitForSeconds(0.2f);

            _audioList.Play(3);

            transform.Find("Turret").gameObject.SetActive(false);
            _animator.Play("Base Layer.Died");

            yield return new WaitForSeconds(1f);

            ServiceLocator.Instance.Get<HUD>().GameOverGroup(true);
        }

        private IEnumerator AnimationOnDamage(Vector3 attackDirection)
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.material = _damageMaterial;

            _body.Impulse(attackDirection * 0.3f);

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.material = _defaultMaterial;
        }
    }
}