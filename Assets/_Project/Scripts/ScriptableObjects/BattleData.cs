using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "ScriptableObjects/BattleData", order = 1)]
public class BattleData : ScriptableObject
{
    public int minimumLevelRequirement; // NOT IMPLEMENTED
    public CharacterData[] enemies;

    public GameObject rewards; // NOT IMPLEMENTED
}

[Serializable]
public class BattleCharacter
{
    public CharacterData characterData;
    public int level; // NOT IMPLEMENTED
}
