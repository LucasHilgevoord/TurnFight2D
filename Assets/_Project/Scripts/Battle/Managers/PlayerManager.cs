using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private CharacterPanel _panelPrefab;
    [SerializeField] private float yOffset = 20;
    private List<CharacterPanel> _panels;

    public CharacterData[] testChars;

    private void Start()
    {
        Initialize(testChars);
    }

    private void OnDisable()
    {
        foreach (CharacterPanel panel in _panels)
        {
            // Stop listening to all the callbacks of the panel
            panel.ActionPerformed -= OnActionPerformed;
        }
    }

    private void Initialize(CharacterData[] characterData)
    {
        _panels = new List<CharacterPanel>();
        foreach (CharacterData data in characterData)
        {
            // Create a new controller for each character in the team
            Character character = new Character(data);

            // Initialize a panel for the character
            GameObject panel = Instantiate(_panelPrefab.gameObject, transform);
            CharacterPanel playerPanel = panel.GetComponent<CharacterPanel>();
            playerPanel.Initialize(character, _panels.Count * yOffset);
            _panels.Add(playerPanel);

            // Start listening to all the callbacks of the panel
            playerPanel.ActionPerformed += OnActionPerformed;
        }
    }

    private void OnActionPerformed(PlayerActions action, CharacterPanel panel)
    {
        Debug.Log("Character: " + panel.Character.Data.name + ", performed action: " + action);

        switch (action)
        {
            case PlayerActions.Attack:
                OnAttack();
                panel.EndAction();
                break;
            case PlayerActions.Special:
                if (panel.Character.SpecialCooldown == 0)
                {
                    OnSpecial();
                    panel.EndAction();
                }
                break;
            case PlayerActions.Defend:
                OnDefend();
                panel.EndAction();
                break;
            case PlayerActions.Info:
                OnInfo(panel.Character);
                break;
            default:
                break;
        }
    }

    private void OnAttack()
    {
        
    }

    private void OnSpecial()
    {
    }

    private void OnDefend()
    {
        
    }

    public InfoHandler infoTest;
    private void OnInfo(Character c)
    {
        infoTest.UpdateInfo(c.Data.specialAbility);
        infoTest.ToggleInfo();
    }
}
