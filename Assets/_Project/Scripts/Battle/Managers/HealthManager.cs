using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private HealthBar _focussedHealthBar;

    [SerializeField] private HealthBar _smallHealthBarPrefab;
    [SerializeField] private GameObject _teamHealthParent;

    internal void Initialize(CharacterData[] characters)
    {
        // Initialize the health bars
        foreach (CharacterData character in characters)
        {
            // Create a new health bar
            HealthBar healthBar = Instantiate(_smallHealthBarPrefab, _teamHealthParent.transform);
        }
    }
}
