using System;

public enum AbilityType
{
    Damage,
    ShieldIncrease, // Add shield
    Heal
}

[Serializable]
public class AbilityEffect : Effect
{
    public AbilityType effectType;
}
