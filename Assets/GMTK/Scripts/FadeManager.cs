using DG.Tweening;
using GMTK.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour, IService
{
    [SerializeField] private Transform _fadeInPanel;
    [SerializeField] private float _fadeInEndValue;
    [SerializeField] private float _fadeOutEndValue;
    [SerializeField] private float _fadeDuration;

    private void Awake() => ServiceLocator.Instance.Register(this);

    private void Start() => FadeOut();

    public void FadeIn(Action onCompleteAction)
    {
        _fadeInPanel.localPosition = new Vector3(_fadeOutEndValue, 0, _fadeInPanel.position.z);
        _fadeInPanel.DOLocalMoveX(_fadeInEndValue, _fadeDuration).OnComplete(() => { onCompleteAction(); });
    }

    public void FadeOut()
    {
        _fadeInPanel.localPosition = new Vector3(_fadeInEndValue, 0, _fadeInPanel.localPosition.z);
        _fadeInPanel.DOLocalMoveX(_fadeOutEndValue, _fadeDuration);
    }
}
