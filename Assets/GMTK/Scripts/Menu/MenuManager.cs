using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _fadeInPanel;

    [SerializeField] private float _fadeInEndValue;
    [SerializeField] private float _fadeOutEndValue;
    [SerializeField] private float _fadeDuration;

    [SerializeField] private PlayableDirector _cutscene;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        DOTween.To(
           () => _fadeInPanel.color.a,
           value => _fadeInPanel.color = new(_fadeInPanel.color.r, _fadeInPanel.color.g, _fadeInPanel.color.b, value),
           0.0f,
           0.3f
       );
    }

    public void Play()
    {
        _cutscene.Play();
        _audioSource.Play();
        StartCoroutine(DelayBeforePlay(1.5f));
    }

    private IEnumerator DelayBeforePlay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        DOTween.To(
            () => _fadeInPanel.color.a,
            value => _fadeInPanel.color = new(_fadeInPanel.color.r, _fadeInPanel.color.g, _fadeInPanel.color.b, value),
            1.0f,
            0.3f
        ).OnComplete(() => LoadGameScene());
    }

    public void LoadGameScene() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit(0);
}
