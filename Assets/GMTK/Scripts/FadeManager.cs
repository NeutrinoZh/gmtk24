using DG.Tweening;
using GMTK.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour, IService
{
    [SerializeField] private Image _fadeInPanel;
    [SerializeField] private float _fadeInEndValue;
    [SerializeField] private float _fadeOutEndValue;
    [SerializeField] private float _fadeDuration;

    private void Awake() => ServiceLocator.Instance.Register(this);

    private void Start() => FadeOut();

    public void FadeIn(Action onCompleteAction)
    {
        DOTween.To(
              () => _fadeInPanel.color.a,
              value => _fadeInPanel.color = new(_fadeInPanel.color.r, _fadeInPanel.color.g, _fadeInPanel.color.b, value),
              1.0f,
              0.3f
        ).OnComplete(() => onCompleteAction());
    }

    public void FadeOut()
    {
        DOTween.To(
              () => _fadeInPanel.color.a,
              value => _fadeInPanel.color = new(_fadeInPanel.color.r, _fadeInPanel.color.g, _fadeInPanel.color.b, value),
              0.0f,
              0.3f
        );
    }
}
