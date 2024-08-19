using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _fadeInPanel;
    [SerializeField] private float _fadeInEndValue;
    [SerializeField] private float _fadeOutEndValue;
    [SerializeField] private float _fadeDuration;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _fadeInPanel.localPosition = new Vector3(_fadeInEndValue, 0);
        _fadeInPanel.DOLocalMoveX(_fadeOutEndValue, _fadeDuration);
    }

    public void Play()
    {
        _audioSource.Play();
        _animator.Play("Base Layer.Player");

        StartCoroutine(DelayBeforeSceneLoad());
    }

    private IEnumerator DelayBeforeSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        _fadeInPanel.localPosition = new Vector3(_fadeOutEndValue, 0);
        _fadeInPanel.DOLocalMoveX(_fadeInEndValue, _fadeDuration).OnComplete(() => LoadGameScene());
    }

    public void LoadGameScene() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit(0);
}
