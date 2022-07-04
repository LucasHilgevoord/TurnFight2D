using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerPanel _panelPrefab;
    [SerializeField] private float yOffset = 20;
    private List<PlayerPanel> _panels;

    public Character[] testChars;

    private void Start()
    {
        Initialize(testChars);
    }

    private void Initialize(Character[] character)
    {
        _panels = new List<PlayerPanel>();
        foreach (Character c in character)
        {
            GameObject panel = Instantiate(_panelPrefab.gameObject, transform);
            PlayerPanel playerPanel = panel.GetComponent<PlayerPanel>();

            playerPanel.SetPosition(_panels.Count * yOffset);
            playerPanel.SetImageCover(c.panelPortraitSprite);
            playerPanel.SetBackgroundTint(c.panelBackgroundTint);

            panel.name = "panel_" + c.name;
            _panels.Add(playerPanel);
        }
    }
}
