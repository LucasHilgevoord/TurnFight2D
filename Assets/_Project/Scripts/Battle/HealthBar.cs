using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform _backgroundRect;
    [SerializeField] private Image _background;
    [SerializeField] private Color _backgroundTint;

    [SerializeField] private Image _barPrefab;
    [SerializeField] private int _barCount;
    [SerializeField] private Color[] _barColors;
    [SerializeField] private Vector2 _barMargin;

    private List<Image> _bars = new List<Image>();

    private void Start()
    {
        _backgroundRect = _background.GetComponent<RectTransform>();
        
        for (int i = 0; i < _barCount; i++)
        {
            GameObject b = Instantiate(_barPrefab.gameObject, transform);
            Image img = b.GetComponent<Image>();

            // Apply the correct scale to the bar
            img.rectTransform.sizeDelta = _backgroundRect.sizeDelta - _barMargin;

            // Set the color of the bar
            if (i > _barColors.Length)
            {
                // Set a default color if there are no colors assigned
                if (_barColors.Length == 0)
                    img.color = Color.white;
                else
                {
                    // Apply the last color assigned
                    img.color = _barColors[_barColors.Length - 1];
                }
            } else
            {
                img.color = _barColors[i];
            }

            _bars.Add(img);
        }
    }

    internal void SetBar(float[] values)
    {
        for (int i = 0; i < _bars.Count; i++)
        {
            _bars[i].fillAmount = values[i];
        }
    }
}
