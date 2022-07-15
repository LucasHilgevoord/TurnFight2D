using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/AbilityData", order = 1)]
public class AbilityData : ScriptableObject
{
    /// <summary> The nhame of this ability. </summary>
    public string abilityName;

    /// <summary> The description of what this ability does. </summary>
    public string abilityDesc;

    /// <summary> How many turns should be passed before able to use this character's special. </summary>
    public int turnCooldown;

    /// <summary> Is the ability special or not. </summary>
    public bool isSpecial;

    /// <summary> Status effects that will be applied once this ability has been used. </summary>
    public StatusEffect[] statusEffects;
    
    /// <summary> Ability that will be used once this ability has been triggered. </summary>
    public AbilityEffect[] abilityEffects;
}
