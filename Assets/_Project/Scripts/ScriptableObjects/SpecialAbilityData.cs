using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpecialAbility", menuName = "ScriptableObjects/SpecialAbilityData", order = 1)]
public class SpecialAbilityData : ScriptableObject
{
    /// <summary> The nhame of this ability. </summary>
    public string abilityName;

    /// <summary> The description of what this ability does. </summary>
    public string abilityDesc;

    /// <summary> How many turns should be passed before able to use this character's special. </summary>
    public int turnCooldown;
}
