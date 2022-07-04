using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private TextMeshProUGUI _cooldown;
    private bool _isOpen;

    private SpecialAbilityData _curData;

    internal void UpdateInfo(SpecialAbilityData data)
    {
        if (_curData == data) return;
        _curData = data;

        _title.text = data.abilityName;
        _desc.text = data.abilityDesc;
        _cooldown.text = "CD: " + data.turnCooldown;
    }

    internal void ToggleInfo()
    {
        _isOpen = !_isOpen;
        if (_isOpen)
            ShowInfo();
        else
            HideInfo();
    }
    internal void ShowInfo() { _panel.SetActive(true); }
    internal void HideInfo() { _panel.SetActive(false); }
}
