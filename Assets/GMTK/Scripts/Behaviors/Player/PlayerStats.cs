using GMTK.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Services
{
    public class PlayerStats : IService
    {
        private List<int> _debugLevels = new(6) { 0, 0, 0, 0, 0, 0 };

        private Dictionary<UpgradeType, int> _levelsOfUpgrades = new();

        private float _speedScale = 1f;

        public float SpeedScale
        {
            get
            {
                return _speedScale;
            }
            set
            {
                _speedScale = value;
            }
        }

        public int Time { get; set; } = 0;
        public Transform Player { get; set; }

        public event Action<UpgradeType> OnUpgrade;

        public void Upgrade(UpgradeType _upgrade)
        {
            if (!_levelsOfUpgrades.ContainsKey(_upgrade))
                _levelsOfUpgrades[_upgrade] = 1;
            _levelsOfUpgrades[_upgrade] += 1;

            _debugLevels[(int)_upgrade] = _levelsOfUpgrades[_upgrade];

            OnUpgrade?.Invoke(_upgrade);
        }

        public int GetLevelUpgrade(UpgradeType _upgrade)
        {
            if (_levelsOfUpgrades.ContainsKey(_upgrade))
                return _levelsOfUpgrades[_upgrade];
            return 1;
        }
    }
}