using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Canvas Elements")]
    [SerializeField] private Image _characterImage;
    [SerializeField] private float _focussedHSV, _unfocussedHSV = 1;

    public Character Character => _character;
    private Character _character;

    internal void Initialize(Character character)
    {
        
    }

    internal void Focus()
    {
        _characterImage.color = Color.HSVToRGB(0, 0, _focussedHSV, true);
    }

    internal void Unfocus()
    {
        _characterImage.color = Color.HSVToRGB(0, 0, _unfocussedHSV, true); ;
    }
}
