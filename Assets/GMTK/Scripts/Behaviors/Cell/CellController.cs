using GMTK.MicroViruses;
using UnityEngine;

namespace GMTK
{
    public class CellController : MonoBehaviour
    {
        [SerializeField] private Sprite _macroWorldSprite;
        [SerializeField] private Sprite _microWorldSprite;

        [SerializeField] private Bounds _entrailsArea;

        private CellStats _cellStats;
        private SpriteRenderer _spriteRenderer;
        private InCellViruses _virusManager;

        private int _virusCount = 0;

        private void Start()
        {
            _cellStats = GetComponent<CellStats>();
            _cellStats.Init();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _virusManager = GetComponentInChildren<InCellViruses>(true);
            _virusManager.SetSpawnArea(_entrailsArea);
        }

        public void AddVirus()
        {
            _virusCount += 2;
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