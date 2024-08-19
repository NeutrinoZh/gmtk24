using GMTK.Services;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI
{
    public enum UpgradeType
    {
        PLAYER_MOVE_SPEED,
        PLAYER_MOBILITY,
        PLAYER_ATTACK_SPEED,
        PLAYER_ARMOR,
        PLAYER_DAMAGE
    };

    public class UpgradesHUD : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _upgradesIcons;
        [SerializeField] private List<string> _upgradesTexts;

        private List<Image> _images = new();
        private List<TextMeshProUGUI> _texts = new();

        private List<UpgradeType> _upgradeTypes = new() {
            UpgradeType.PLAYER_MOVE_SPEED, UpgradeType.PLAYER_DAMAGE, UpgradeType.PLAYER_ARMOR
        };

        private void Awake()
        {
            for (int i = 0; i < 3; ++i)
            {
                int index = i;

                var element = transform.GetChild(i);

                element.GetComponent<Button>().onClick.AddListener(() => OnClick(index));
                _images.Add(element.GetComponent<Image>());
                _texts.Add(element.GetComponentInChildren<TextMeshProUGUI>());
            }
        }

        private void OnEnable()
        {
            var types = new List<UpgradeType>((UpgradeType[])Enum.GetValues(typeof(UpgradeType)));
            for (int i = 0; i < 3; ++i)
            {
                int at = UnityEngine.Random.Range(0, types.Count);
                _upgradeTypes[i] = types[at];
                types.RemoveAt(at);
            }

            for (int i = 0; i < 3; ++i)
            {
                _images[i].sprite = _upgradesIcons[(int)_upgradeTypes[i]];
                _texts[i].text = _upgradesTexts[(int)_upgradeTypes[i]];
            }
        }

        private void OnClick(int index)
        {
            ServiceLocator.Instance.Get<PlayerStats>().Upgrade(_upgradeTypes[index]);
            ServiceLocator.Instance.Get<HUD>().UpgradeDialog(false);
        }
    }
}