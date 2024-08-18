using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform _fadeInPanel;
    [SerializeField] private float _fadeInEndValue;
    [SerializeField] private float _fadeOutEndValue;
    [SerializeField] private float _fadeDuration;

    void Start() 
    {
        _fadeInPanel.localPosition = new Vector3(_fadeInEndValue, 0);
        _fadeInPanel.DOLocalMoveX(_fadeOutEndValue, _fadeDuration);
    }

    public void Play()
    {
        _fadeInPanel.localPosition = new Vector3(_fadeOutEndValue, 0);
        _fadeInPanel.DOLocalMoveX(_fadeInEndValue, _fadeDuration).OnComplete(() => LoadGameScene());
    }

    public void LoadGameScene() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit(0);
}
