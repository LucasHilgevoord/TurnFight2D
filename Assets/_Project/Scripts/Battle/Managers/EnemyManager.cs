using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy[] _characters;
    [SerializeField] private float rowScalingOffset = 0.25f;
    [SerializeField] private float _maxOffsetX = 200;
    
    private Vector3[] _positions;
    private float[] _scaling;
    private int _currentFocusIndex;

    private void Start()
    {
        Initialize(_characters);
    }

    private void Update()
    {
        // TESTING
        if (Input.GetKeyDown(KeyCode.RightArrow))
            FocusNext();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            FocusPrevious();
    }

    private void Initialize(Enemy[] characters)
    {
        _characters = characters;
        _currentFocusIndex = 0;
    }

    private void FocusNext()
    {
        Debug.Log("Focus prev");

        // Unfocus the previous one
        _characters[_currentFocusIndex].Unfocus();

        // Go to the next one
        _currentFocusIndex = _currentFocusIndex >= _characters.Length - 1 ? 0 : _currentFocusIndex + 1;
        _characters[_currentFocusIndex].Focus();
    }

    private void FocusPrevious()
    {
        Debug.Log("Focus prev");

        // Unfocus the previous one
        _characters[_currentFocusIndex].Unfocus();

        // Go to the previous one
        _currentFocusIndex = _currentFocusIndex <= 0 ? _characters.Length - 1 : _currentFocusIndex - 1;
        _characters[_currentFocusIndex].Focus();
    }

    private void FocusSpecific(Enemy character)
    {
        int index = -1;
        for (int i = 0; i < _characters.Length - 1; i++)
        {
            if (character == _characters[i]) 
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
