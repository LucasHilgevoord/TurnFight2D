using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    private string _wavePrefix = "Wave ";
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveNumberGUI;

    private string _turnPrefix = "Turn ";
    [SerializeField] private TextMeshProUGUI _turnText;
    [SerializeField] private TextMeshProUGUI _turnNumberGUI;

    private int _waveNumber;
    private int _turnNumber;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _waveNumber = _turnNumber = 0;
        NextWave();
        NextTurn();
    }

    internal void NextWave()
    {
        _waveNumber++;
        _waveNumberGUI.text = _waveNumber.ToString();
    }

    internal void NextTurn()
    {
        _turnNumber++;
        _turnNumberGUI.text = _turnNumber.ToString();
    }
}
