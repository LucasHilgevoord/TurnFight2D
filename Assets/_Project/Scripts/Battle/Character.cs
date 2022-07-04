using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Alive,
    Dead
}

public class Character
{
    internal CharacterData Data => _characterData;
    private CharacterData _characterData;

    private int _maxHealth => _characterData.maxHealth;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    private int _minSpecialCooldown => _characterData.specialAbility.turnCooldown;
    private int _specialCooldown;
    public int SpecialCooldown => _specialCooldown;

    public Character(CharacterData character)
    {
        _characterData = character;
        
        // Set starting values
        _currentHealth = _maxHealth;
        _specialCooldown = _minSpecialCooldown;
    }
}
