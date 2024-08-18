using UnityEngine;

namespace GMTK
{
    public class CellController : MonoBehaviour
    {
        [SerializeField] private Sprite _macroWorldSprite;
        [SerializeField] private Sprite _microWorldSprite;

        private CellStats _cellStats;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _cellStats = GetComponent<CellStats>();
            _cellStats.Init();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetMacroSprite()
        {
            _spriteRenderer.sprite = _macroWorldSprite;
        }

        public void SetMicroSprite()
        {
            _spriteRenderer.sprite = _microWorldSprite;
        }
    }
}