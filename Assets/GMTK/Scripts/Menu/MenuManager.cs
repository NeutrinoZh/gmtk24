using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _fadeInPanel;
    [SerializeField] private float _fadeInEndValue;
    [SerializeField] private float _fadeOutEndValue;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private PlayableDirector _cutscene;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Color color = _fadeInPanel.color;
        color.a = Mathf.Clamp01(0);
        _fadeInPanel.color = color;
    }

    public void Play()
    {
        _cutscene.Play();
        _audioSource.Play();
        StartCoroutine(DelayBeforePlay(1f));
    }

    private void PlayGame()
    {
        StartCoroutine(DelayBeforeSceneLoad());
    }

    private IEnumerator DelayBeforeSceneLoad()
    {
        yield return new WaitForSeconds(0.5f);
        _fadeInPanel.localPosition = new Vector3(_fadeOutEndValue, 0);
        _fadeInPanel.DOLocalMoveX(_fadeInEndValue, _fadeDuration).OnComplete(() => LoadGameScene());
    }

    private IEnumerator DelayBeforePlay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        PlayGame();
    }

    public void LoadGameScene() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit(0);
}
