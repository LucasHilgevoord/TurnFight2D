using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private HealthBar _focussedHealthBar;
    private Character _focussedCharacter;

    [SerializeField] private HealthBar _smallHealthBarPrefab;
    [SerializeField] private GameObject _teamHealthParent;

    private Character[] _characters;

    private void OnDestroy()
    {
        foreach (Character c in _characters)
        {
            c.HealthUpdated -= OnHealthUpdated;
        }
    }

    internal void Initialize(Character[] characters)
    {
        _characters = characters;
        Debug.Log(_characters.Length);

        // Initialize the health bars
        foreach (Character c in characters)
        {
            // Create a new health bar
            HealthBar healthBar = Instantiate(_smallHealthBarPrefab, _teamHealthParent.transform);
            healthBar.InitializeBar(c.Data.maxHealth, c.CurrentHealth);
        }
    }

    internal void ChangeFocussedBar(Character character)
    {
        float health = character.CurrentHealth / character.Data.maxHealth;
        float shield = character.CurrentShield / character.Data.maxShield;
        
        if (_focussedCharacter != null)
            _focussedCharacter.HealthUpdated -= OnFocussedHealthUpdated;

        _focussedCharacter = character;
        _focussedHealthBar.InitializeBar(character.Data.maxHealth, character.CurrentHealth);
        character.HealthUpdated += OnFocussedHealthUpdated;
    }

    private void OnFocussedHealthUpdated(float newHealth)
    {
        _focussedHealthBar.UpdateBar(newHealth);
    }

    private void OnHealthUpdated(float obj)
    {
        throw new NotImplementedException();
    }
}
