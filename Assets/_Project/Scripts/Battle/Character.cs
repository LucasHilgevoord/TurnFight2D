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

    private int _maxShield => _characterData.maxShield;
    private int _currentShield = 0;
    public int CurrentShield => _currentShield;

    private int _minSpecialCooldown => _characterData.specialAbility.turnCooldown;
    private int _specialCooldown;
    public int SpecialCooldown => _specialCooldown;

    private StatusEffect[] _activeEffects;

    public bool IsDefending { get { return _isDefending; } set { _isDefending = value; } }
    private bool _isDefending;

    public Character(CharacterData character)
    {
        _characterData = character;
        
        // Set starting values
        _currentHealth = _maxHealth;
        _specialCooldown = _minSpecialCooldown;
    }

    public void DamageSelf(int points) 
    {
        // Check if we still have shield to block the attack
        if (_currentShield > 0)
        {
            if (points > _currentShield)
            {
                // Enough shield to receive the damage on the shield
                UpdateShield(_currentShield - points);
            }
            else
            {
                // Not enough shield to block all the damage, remove both shield and health
                points -= _currentShield;
                UpdateShield(0);
                UpdateHealth(_currentHealth - points);
            }
        }
        else
        {
            // We have no shield so remove health
            UpdateHealth(_currentHealth - points);
        }
    }

    public void HealSelf(int points) { UpdateHealth(_currentHealth + points); }

    private void UpdateHealth(int points) { _currentHealth = Mathf.Clamp(points, 0, _maxHealth); Debug.Log("Health Updated to: " + _currentHealth); }

    public void SubtractShield(int points) { UpdateShield(_currentShield - points); }
    
    public void AddShield(int points) { UpdateShield(_currentShield + points); Debug.Log(points); }

    public void UpdateShield(int points) { _currentShield = Mathf.Clamp(points, 0, _maxShield); Debug.Log("Shield Updated to: " + _currentShield); }

}
