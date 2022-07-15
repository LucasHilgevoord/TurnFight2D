using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    /// <summary> The name of the character. </summary>
    public string characterName;

    /// <summary> Background story of the character. </summary>
    public string characterDesc;

    /// <summary> The damage type of the character. </summary>
    public ElementTypes damageType;

    ///// <summary> How much damage the character does in a normal strikes. </summary>
    //public int damageAmount;

    /// <summary> The max health of the character. </summary>
    public int maxHealth;

    /// <summary> The max shield of the character. </summary>
    public int maxShield;

    /// <summary> Data of what the special does. </summary>
    public AbilityData specialAbility;
    
    /// <summary> Data of what the special does. </summary>
    public AbilityData primaryAbility;

    [Header("Panel")]
    /// <summary> The sprite that will be displayed in the character panel. </summary>
    public Sprite panelPortraitSprite;

    /// <summary> The background tint behind the character's portrait. </summary>
    public Color panelBackgroundTint;

    [Header("Battlefield")]
    public Sprite battleSprite;
}
