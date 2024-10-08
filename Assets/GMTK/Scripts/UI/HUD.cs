using GMTK.Services;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GMTK.UI
{
    public class HUD : MonoBehaviour, IService
    {
        private Transform _adviceGetOut;
        private Transform _gameOverGroup;
        private Transform _upgradeGroup;
        private Transform _barsGroup;

        private InputController _inputController;
        private PlayerStats _playerStats;

        private TextMeshProUGUI _timeTextMesh;

        private TextMeshProUGUI _statisticsTextMesh;
        private string _statisticsTextPattern;

        public void AdviceGetOutSet(bool isActive)
        {
            _adviceGetOut.gameObject.SetActive(isActive);
        }

        public void GameOverGroup(bool isActive)
        {
            _gameOverGroup.gameObject.SetActive(isActive);

            if (ServiceLocator.Instance.Get<CellManager>().Pool.Count == 0)
                _gameOverGroup.Find("YouDied").GetComponent<TextMeshProUGUI>().text = "All Cells Have Died";

            _upgradeGroup.gameObject.SetActive(false);
            _barsGroup.gameObject.SetActive(false);
        }

        public void UpgradeDialog(bool isActive)
        {
            _upgradeGroup.gameObject.SetActive(isActive);
            _timeTextMesh.gameObject.SetActive(!isActive);
            _barsGroup.gameObject.SetActive(!isActive);
        }

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);

            _playerStats = ServiceLocator.Instance.Get<PlayerStats>();
            _inputController = ServiceLocator.Instance.Get<InputController>();
            _inputController.Actions.GameOver.AnyKey.performed += AnyKeyHandle;

            _adviceGetOut = transform.Find("AdviceGetOutText");
            _adviceGetOut.gameObject.SetActive(false);

            _gameOverGroup = transform.Find("GameOverGroup");
            _gameOverGroup.gameObject.SetActive(false);

            _upgradeGroup = transform.Find("UpgradeGroup");
            _upgradeGroup.gameObject.SetActive(false);

            _barsGroup = transform.Find("Bars");

            _timeTextMesh = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
            StartCoroutine(StartTime());

            _statisticsTextMesh = transform.Find("Statistics").GetComponent<TextMeshProUGUI>();
            _statisticsTextPattern = _statisticsTextMesh.text;
            ServiceLocator.Instance.Get<GameStatistics>().OnStatisticsChange += (int livingCells, int sickCells, int diedCells) =>
            {
                _statisticsTextMesh.text = _statisticsTextPattern
                    .Replace("{lc}", $"{livingCells:D2}")
                    .Replace("{sc}", $"{sickCells:D2}")
                    .Replace("{cd}", $"{diedCells:D2}");
            };
        }

        private IEnumerator StartTime()
        {
            while (true)
            {
                _playerStats.Time += 1;

                int minutes = _playerStats.Time / 60;
                int seconds = _playerStats.Time % 60;

                _timeTextMesh.text = $"{minutes:D2}:{seconds:D2}";

                yield return new WaitForSeconds(1);
            }
        }

        private void OnDestroy()
        {
            _inputController.Actions.GameOver.AnyKey.performed -= AnyKeyHandle;
        }

        private void AnyKeyHandle(InputAction.CallbackContext ctx)
        {
            if (!_gameOverGroup.gameObject.activeSelf)
                return;

            ServiceLocator.Instance.Get<FadeManager>().FadeIn(() => SceneManager.LoadScene(0));
        }
    }
}