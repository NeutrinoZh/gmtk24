using GMTK.Services;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI
{
    public class BarsHUD : MonoBehaviour
    {
        private Image _healthBarFilled;
        private Image _armorBarFilled;

        private PlayerDamageable _player;

        private void Awake()
        {
            _healthBarFilled = transform.GetChild(0).GetChild(1).GetComponent<Image>();
            _armorBarFilled = transform.GetChild(1).GetChild(1).GetComponent<Image>();

        }

        private void Start()
        {
            _player = ServiceLocator.Instance.Get<PlayerStats>().Player.GetComponent<PlayerDamageable>();
        }

        private void Update()
        {
            _healthBarFilled.fillAmount = _player.Health * 0.13f;
            _armorBarFilled.fillAmount = _player.Armor * 0.13f;
        }
    }
}