using GMTK.Services;
using System;
using System.Collections.Generic;
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
        [SerializeField] private List<Image> _upgradesIcons;
        private List<UpgradeType> _upgradeTypes = new() {
            UpgradeType.PLAYER_MOVE_SPEED, UpgradeType.PLAYER_DAMAGE, UpgradeType.PLAYER_ARMOR
        };

        private void Start()
        {
            for (int i = 0; i < 3; ++i)
            {
                int index = i;
                transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => OnClick(index));
            }
        }

        private void OnEnable()
        {
            var types = new List<UpgradeType>((UpgradeType[])Enum.GetValues(typeof(UpgradeType)));
            for (int i = 0; i < 3; ++i)
            {
                _upgradeTypes[i] = types[UnityEngine.Random.Range(0, types.Count)];
                types.Remove(types[i]);
            }
        }

        private void OnClick(int index)
        {
            ServiceLocator.Instance.Get<PlayerStats>().Upgrade(_upgradeTypes[index]);
            ServiceLocator.Instance.Get<HUD>().UpgradeDialog(false);
        }
    }
}