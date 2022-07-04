using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;
    [SerializeField] private float rowScalingOffset = 0.25f;
    [SerializeField] private float _maxOffsetX = 200;

    private Vector3[] _positions;
    private float[] _scaling;
    private int _currentIndex;

    private void Start()
    {
        Initialize(_characters);
    }

    private void Initialize(GameObject[] characters)
    {
        _characters = characters;
        int dir = -1;
        //float curScale = 1;
        
        for (int i = 0; i < _characters.Length; i++)
        {
            //GameObject c = Instantiate(_characters[i], transform);
            
            if (i != 0)
            {
                //curScale = 1 - (i * rowScalingOffset);
                //c.transform.localScale = new Vector3(curScale , curScale, 1);
            }
        }

        _currentIndex = 0;
    }

    private void FocusNextEnemy()
    {
        
    }

    private void FocusPreviousEnemy()
    {
        
    }

    private void FocusSpecificEnemy(GameObject character)
    {
        
    }

    /// <summary>
    /// Method to damage the currently focussed enemy
    /// </summary>
    private void DamageEnemy(float damage)
    {
        
    }
}
