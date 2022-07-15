using System;

public enum StatusEffectType
{
    Attack,
    Defence,
    Recovery
}

[Serializable]
public class StatusEffect : Effect
{
    public StatusEffectType effectType;
}
