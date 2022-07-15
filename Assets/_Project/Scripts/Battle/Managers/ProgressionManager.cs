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

    private int _waveNubmer;
    private int _turnNumber;
}
