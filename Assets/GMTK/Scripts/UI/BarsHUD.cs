using DG.Tweening;
using GMTK.Services;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI
{
    public class BarsHUD : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshLevel;
        private Image _healthBarFilled;
        private Image _armorBarFilled;
        private Image _experienceFilled;

        private PlayerDamageable _player;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _healthBarFilled = transform.GetChild(0).GetChild(1).GetComponent<Image>();
            _armorBarFilled = transform.GetChild(1).GetChild(1).GetComponent<Image>();
            _experienceFilled = transform.GetChild(2).GetChild(1).GetComponent<Image>();
            _textMeshLevel = transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _player = _playerStats.Player.GetComponent<PlayerDamageable>();
            _playerStats.OnExperienceChange += OnExperienceChange;
            _experienceFilled.fillAmount = 0;
        }

        private void OnDestroy()
        {
            _playerStats.OnExperienceChange -= OnExperienceChange;
        }

        private void Update()
        {
            _healthBarFilled.fillAmount = _player.Health * 0.13f;
            _armorBarFilled.fillAmount = _player.Armor * 0.13f;
            _textMeshLevel.text = _playerStats.Level.ToString();
        }

        private void OnExperienceChange(bool isNewLevel)
        {
            DOTween.To(
                () => _experienceFilled.fillAmount,
                value => _experienceFilled.fillAmount = value,
                isNewLevel ? 1 : _playerStats.Experience / (float)_playerStats.MaxExperience,
                0.3f
            ).OnComplete(() =>
            {
                if (isNewLevel)
                {
                    ServiceLocator.Instance.Get<HUD>().UpgradeDialog(true);
                    _experienceFilled.fillAmount = 0;
                }
            });
        }
    }
}