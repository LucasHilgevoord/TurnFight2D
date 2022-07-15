using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public List<Character> Characters => _characters;
    protected List<Character> _characters;
    internal int _currentFocusIndex;

    internal virtual void Initialize(CharacterData[] characterData)
    {
        _characters = new List<Character>();

        foreach (CharacterData data in characterData)
        {
            // Create a new controller for each character in the team
            Character character = new Character(data);
            _characters.Add(character);
        }
    }
}
