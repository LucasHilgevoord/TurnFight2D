using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : TeamManager
{
    [SerializeField] private CharacterPanel _panelPrefab;
    [SerializeField] private float yOffset = 20;
    private List<CharacterPanel> _panels;
    private int _usedAbility = 0;

    private void OnDisable()
    {
        SignalBus.Unsubscribe<EvPanelAction>(OnActionReceived);
    }

    internal override void Initialize(CharacterData[] characterData)
    {
        base.Initialize(characterData);
        _panels = new List<CharacterPanel>();

        // Flip the array so the first one will be displayed on the left
        //Array.Reverse(characterData);
        
        foreach (Character character in _characters)
        {
            // Initialize a panel for the character
            CharacterPanel playerPanel = Instantiate(_panelPrefab, transform);
            playerPanel.Initialize(character, _panels.Count * yOffset);
            _panels.Add(playerPanel);
        }

        SignalBus.Subscribe<EvPanelAction>(OnActionReceived);
    }

    private void OnActionReceived(EvPanelAction signal)
    {
        Debug.Log("Character: " + signal.panel.Character.Data.name + ", performed action: " + signal.action);
        Character c = signal.panel.Character;
        switch (signal.action)
        {
            case PanelInput.Click:
                OnAttack(c);
                EndAction(signal.panel);
                break;
            case PanelInput.SwipeUp:
                if (c.SpecialCooldown == 0)
                {
                    OnSpecial(c);
                    EndAction(signal.panel);
                }
                break;
            case PanelInput.SwipeDown:
                OnDefend(c);
                EndAction(signal.panel);
                break;
            case PanelInput.Hold:
                OnInfo(signal.panel.Character);
                break;
            default:
                break;
        }
    }

    internal void EnablePanels()
    {
        foreach (CharacterPanel panel in _panels)
        {
            panel.EnablePanel();
        }
    }

    private void EndAction(CharacterPanel panel)
    {
        panel.EndAction();
        _usedAbility++;

        if (_usedAbility == _characters.Count)
        {
            _usedAbility = 0;
            SignalBus.Broadcast(new EvPlayerTurnFinished());
        }
    }

    private void OnAttack(Character c)
    {
        SignalBus.Broadcast(new EvPlayerAbility(c.Data.primaryAbility, c));
    }

    private void OnSpecial(Character c)
    {
        SignalBus.Broadcast(new EvPlayerAbility(c.Data.specialAbility, c));
    }

    private void OnDefend(Character c)
    {
        c.IsDefending = true;
    }

    public InfoHandler infoTest;
    private void OnInfo(Character c)
    {
        infoTest.UpdateInfo(c.Data.specialAbility);
        infoTest.ToggleInfo();
    }
}
