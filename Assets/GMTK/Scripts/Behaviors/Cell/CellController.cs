using DG.Tweening;
using GMTK.GameStates;
using GMTK.MicroViruses;
using GMTK.Services;
using GMTK.UI;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK
{
    public class CellController : MonoBehaviour
    {
        private const float k_poisonCoroutineTimeStep = 1f;

        [SerializeField] private Texture _macroWorldSprite;
        [SerializeField] private Sprite _macroWorldBadSprite;
        [SerializeField] private Texture _microWorldSprite;
        [SerializeField] private Sprite _microWorldBadSprite;

        [SerializeField] private Bounds _entrailsArea;
        [SerializeField] private Vector2 _maxVelocity;

        private AudioSource _audioSource;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private InCellViruses _inCellVirusManager;
        private VirusSpawner _virusSpawner;
        private CellManager _cellManager;
        private GameStatistics _gameStatistics;
        private Rigidbody2D _rb;
        private int _virusCount = 0;
        private VirusCellManager _virusCellManager;

        private float _health = 100f;

        public bool IsAlive => _health > 0;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _inCellVirusManager = GetComponentInChildren<InCellViruses>(true);
            _cellManager = ServiceLocator.Instance.Get<CellManager>();
            _virusSpawner = ServiceLocator.Instance.Get<VirusSpawner>();
            _gameStatistics = ServiceLocator.Instance.Get<GameStatistics>();
            _virusCellManager = ServiceLocator.Instance.Get<VirusCellManager>();

            _inCellVirusManager.GetComponent<InCellVirusManager>().OnObjectRemoved += OnDestroyVirus;

            SetVelocity(WorldState.MACRO_WORLD);

            _animator.enabled = false;

            _spriteRenderer.material.SetFloat("_Blend", 1f);
            StartCoroutine(PoisonCoroutine());

            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void OnAddVirus()
        {
            if (_virusCount == 0)
            {
                _gameStatistics.CellSick();
                _virusCellManager.AddObject(transform);
            }

            _virusCount += 1;
        }

        public void OnDestroyVirus()
        {
            _virusCount -= 1;

            if (_virusCount == 0)
            {
                _health = 100;
                _virusCellManager.RemoveObject(transform);
                _gameStatistics.CellRecovered();
            }
        }

        private IEnumerator PoisonCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(k_poisonCoroutineTimeStep);

                _health -= _virusCount;

                if (_health <= 0)
                {
                    Die();
                    yield break;
                }

                DOTween.To(
                    () => _spriteRenderer.material.GetFloat("_Blend"),
                    blend => _spriteRenderer.material.SetFloat("_Blend", blend),
                    _health / 100,
                    k_poisonCoroutineTimeStep
                );
            }
        }

        private void Die()
        {
            var player = ServiceLocator.Instance.Get<PlayerStats>().Player;

            if (player != null)
            {
                var nearestCell = _cellManager.FindNearToPoint(player.position);
                if (nearestCell == transform)
                    ServiceLocator.Instance.Get<GamePlayState>().State = WorldState.MACRO_WORLD;
            }

            _cellManager.RemoveObject(transform);
            Destroy(gameObject, 2f);

            _animator.enabled = true;
            _animator.Play("Base Layer.Dead");

            _audioSource.Play();

            if (_cellManager.Pool.Count == 0)
                ServiceLocator.Instance.Get<HUD>().GameOverGroup(true);
            else
                StartCoroutine(SpawnVirusesByDelay());
        }

        private IEnumerator SpawnVirusesByDelay()
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _virusCount + 1; ++i)
                _virusSpawner.SpawnVirus(transform.position);
        }

        public void SetVelocity(WorldState state)
        {
            switch (state)
            {
                case WorldState.MACRO_WORLD:
                    _rb.isKinematic = false;
                    _rb.velocity = new Vector3(Random.Range(-_maxVelocity.x, _maxVelocity.x), Random.Range(-_maxVelocity.y, _maxVelocity.y), 0);
                    break;
                case WorldState.MICRO_WORLD:
                    _rb.velocity = Vector3.zero;
                    _rb.angularVelocity = 0;
                    _rb.isKinematic = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetMacroSprite()
        {
            _spriteRenderer.sprite = _macroWorldBadSprite;
            _spriteRenderer.material.SetTexture("_SecondaryTex", _macroWorldSprite);

            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void SetMicroSprite()
        {
            _spriteRenderer.sprite = _microWorldBadSprite;
            _spriteRenderer.material.SetTexture("_SecondaryTex", _microWorldSprite);

            transform.GetChild(0).gameObject.SetActive(true);
            ShuffleEntrails();
            _inCellVirusManager.SetVirusesCount(_virusCount);
        }

        public void ShuffleEntrails()
        {
            var entrails = transform.GetChild(0).Find("Entrails");
            foreach (Transform entrail in entrails)
            {
                var position = new Vector3(
                    Random.Range(_entrailsArea.min.x, _entrailsArea.max.x),
                    Random.Range(_entrailsArea.min.y, _entrailsArea.max.y),
                    0
                );

                entrail.localPosition = position;
                if (entrail.TryGetComponent(out Rigidbody2D rd))
                {
                    rd.velocity = new Vector3(
                        Random.Range(-0.2f, 0.2f),
                        Random.Range(-0.2f, 0.2f),
                        0
                    );
                }
            }

        }
    }
}