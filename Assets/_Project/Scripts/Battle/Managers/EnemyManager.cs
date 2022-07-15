using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : TeamManager
{
    public List<FieldEnemy> FieldEnemies => _fieldEnemies;
    private List<FieldEnemy> _fieldEnemies;

    [SerializeField] private FieldEnemy _enemyPrefab;
    //[SerializeField] private float rowScalingOffset = 0.25f;
    //[SerializeField] private float _maxOffsetX = 200;
    
    //private Vector3[] _positions;
    //private float[] _scaling;

    private void Update()
    {
        // TESTING
        if (Input.GetKeyDown(KeyCode.RightArrow))
            FocusNext();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            FocusPrevious();
    }

    internal override void Initialize(CharacterData[] characterData)
    {
        base.Initialize(characterData);
        _fieldEnemies = new List<FieldEnemy>();

        foreach (Character character in _characters)
        {
            // Initialize a panel for the character
            FieldEnemy fieldEnemy = Instantiate(_enemyPrefab, transform);
            fieldEnemy.Initialize(character);
            _fieldEnemies.Add(fieldEnemy);
            
        }  
        _currentFocusIndex = 0;
    }

    private void FocusNext()
    {
        Debug.Log("Focus prev");

        // Unfocus the previous one
        _fieldEnemies[_currentFocusIndex].Unfocus();

        // Go to the next one
        _currentFocusIndex = _currentFocusIndex >= _fieldEnemies.Count ? 0 : _currentFocusIndex + 1;
        _fieldEnemies[_currentFocusIndex].Focus();
    }

    private void FocusPrevious()
    {
        Debug.Log("Focus prev");

        // Unfocus the previous one
        _fieldEnemies[_currentFocusIndex].Unfocus();

        // Go to the previous one
        _currentFocusIndex = _currentFocusIndex <= 0 ? _fieldEnemies.Count : _currentFocusIndex - 1;
        _fieldEnemies[_currentFocusIndex].Focus();
    }

    private void FocusSpecific(FieldEnemy character)
    {
        int index = -1;
        for (int i = 0; i < _fieldEnemies.Count; i++)
        {
            if (character == _fieldEnemies[i]) 
            { 
                index = i; 
                break; 
            }
        }
        
        if (index != -1)
        {
            FocusSpecific(index);
        } else
        {
            Debug.Log("Requested character to focus has not been found!");
        }
    }

    private void FocusSpecific(float index)
    {

    }

    /// <summary>
    /// Method to damage the currently focussed enemy
    /// </summary>
    private void DamageEnemy(float damage)
    {
        
    }
}
