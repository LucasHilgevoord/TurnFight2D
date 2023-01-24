using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Character _observedCharacter;
    private float _maxValue, _currentValue;

    private RectTransform _backgroundRect;
    [SerializeField] private Image _background;
    [SerializeField] private Color _backgroundTint;

    [SerializeField] private Image _barPrefab;
    [SerializeField] private Color _barColor;
    [SerializeField] private Vector2 _barMargin;
    [SerializeField] private float _updateSpeed = 2;
    private Image _bar;

    private void Start()
    {
        _backgroundRect = _background.GetComponent<RectTransform>();
        _bar = Instantiate(_barPrefab, transform);

        // Apply the correct scale to the baraaa
        _bar.rectTransform.sizeDelta = _backgroundRect.sizeDelta - _barMargin;
        _bar.color = _barColor;
    }

    internal void InitializeBar(float maxValue, float startValue) 
    {
        _maxValue = maxValue;
        UpdateBar(startValue);
    }

    internal void UpdateBar(float value)
    {
        _currentValue = value;
        _bar.DOFillAmount(_currentValue / _maxValue, _updateSpeed).SetSpeedBased().SetEase(Ease.OutSine);
    }
}
