using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldEnemy : MonoBehaviour
{
    [Header("Canvas Elements")]
    [SerializeField] private Image _characterImage;
    [SerializeField] private float _focussedHSV, _unfocussedHSV = 1;

    public Character Character => _character;
    private Character _character;

    [SerializeField] private HealthBar _healthBar;

    internal void Initialize(Character character)
    {
        // Set the character
        _character = character;

        // Set the name for the inspector
        gameObject.name = "enemy_" + _character.Data.name;
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
