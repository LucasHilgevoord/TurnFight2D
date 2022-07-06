using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Heal,
    Attack,
    Defence
}

public enum EffectOperator
{
    Decrease,
    Increase,
    Add,
    Subtract,
    Multiply
}

public enum EffectTarget
{
    Self,
    PlayerTeam,
    PlayerRandom,
    EnemyTarget,
    EnemyTeam,
    EnemyRandom
}

public class StatusEffect : MonoBehaviour
{
    public EffectType effectType;
    public EffectOperator effectOperator;
    public EffectTarget effectTarget;

    public float amount;
}
