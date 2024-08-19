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

        private int _maxExperience = 3;
        private int _experience = 0;
        private int _level = 1;

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

        public int MaxExperience => _maxExperience;
        public int Experience => _experience;
        public int Level => _level;

        public int Time { get; set; } = 0;
        public Transform Player { get; set; }

        public event Action<bool> OnExperienceChange;
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

        public void AddExperience()
        {
            bool isNewLevel = false;

            _experience += 1;

            if (_experience >= _maxExperience)
            {
                _level += 1;
                _experience = 0;
                _maxExperience = (int)(_maxExperience * 1.5f);

                isNewLevel = true;
            }

            OnExperienceChange?.Invoke(isNewLevel);
        }
    }
}