using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialHandler : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Image _characterImage;

    internal void UpdatePanel(string abilityName, Sprite characterSprite)
    {
        _title.text = abilityName;
        _characterImage.sprite = characterSprite;
    }

    internal void ShowPanel()
    {
        _panel.SetActive(true);
        Invoke(nameof(HidePanel), 2f);
    }

    internal void HidePanel()
    {
        _panel.SetActive(false);
    }
}
