using UnityEngine;

public struct EvPanelAction
{
    public PanelInput action;
    public CharacterPanel panel;

    public EvPanelAction(PanelInput action, CharacterPanel panel)
    {
        this.action = action;
        this.panel = panel;
    }
}

public struct EvPlayerAbility
{
    public AbilityData ability;
    public Character caster;

    public EvPlayerAbility(AbilityData ability, Character caster)
    {
        this.ability = ability;
        this.caster = caster;
    }
}

public struct EvEnemyFocusChanged
{
    public Character focus;

    public EvEnemyFocusChanged(Character focus)
    {
        this.focus = focus;
    }
}

