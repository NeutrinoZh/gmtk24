using GMTK.GameStates;
using GMTK.MicroViruses;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK
{
    public class CellController : MonoBehaviour
    {
        [SerializeField] private Sprite _macroWorldSprite;
        [SerializeField] private Sprite _microWorldSprite;
        [SerializeField] private Bounds _entrailsArea;
        [SerializeField] private Vector2 _maxVelocity;

        private SpriteRenderer _spriteRenderer;
        private InCellViruses _virusManager;
        private Rigidbody2D _rb;
        private int _virusCount = 0;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _virusManager = GetComponentInChildren<InCellViruses>(true);

            SetVelocity(WorldState.MACRO_WORLD);
        }

        public void AddVirus()
        {
            _virusCount += 2;
        }

        public void SetVelocity(WorldState state)
        {
            switch (state)
            {
                case WorldState.MACRO_WORLD:
                    _rb.drag = 0f;
                    _rb.velocity = new Vector3(Random.Range(-_maxVelocity.x, _maxVelocity.x), Random.Range(-_maxVelocity.y, _maxVelocity.y), 0);
                    break;
                case WorldState.MICRO_WORLD:
                    _rb.drag = 10f;
                    _rb.velocity = Vector2.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetMacroSprite()
        {
            _spriteRenderer.sprite = _macroWorldSprite;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void SetMicroSprite()
        {
            _spriteRenderer.sprite = _microWorldSprite;
            transform.GetChild(0).gameObject.SetActive(true);
            ShuffleEntrails();
            _virusManager.SetVirusesCount(_virusCount);
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