using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerActions
{
    Attack,
    Special,
    Defend,
    Info
}

public class CharacterPanel : MonoBehaviour
{
    public event Action<PlayerActions, CharacterPanel> ActionPerformed;

    private bool _isEnabled = true;
    private float _startY = 0;

    [Header("Panel components")]
    [SerializeField] private RectTransform _panel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _charImage;
    [SerializeField] private float _enabledHSV, _disabledHSV = 1;

    [Header("Icons")]
    [SerializeField] private CanvasGroup _shieldIcon;
    [SerializeField] private CanvasGroup _specialIcon;

    [Header("Character")]
    private bool _infoVisible;
    public Character Character => _character;
    private Character _character;

    [Header("Input Actions")]
    [SerializeField] private float _maxDrag = 20;
    private Vector3 _dragStartPos;
    private bool _onDrag;

    private bool _onPointerDown;
    private float _pointDownTimer;
    [SerializeField] private float _mixHoldForInfo = 1;

    internal void Initialize(Character character, float startY)
    {
        // Set the panel at the right height
        _startY = startY;
        _panel.anchoredPosition = new Vector3(0, _startY, 0);

        // Set the character
        _character = character;

        // Set the correct sprites
        _charImage.sprite = _character.Data.panelPortraitSprite;
        _backImage.color = _character.Data.panelBackgroundTint;

        // Set the name for the inspector
        _panel.name = "panel_" + _character.Data.name;
    }

    private void EnablePanel()
    {
        _isEnabled = true;
        _canvasGroup.interactable = true;

        // Reset the colors
        Color c = Color.HSVToRGB(0, 0, _enabledHSV, true);
        _backImage.color = c;
        _charImage.color = c;
    }

    private void DisablePanel()
    {
        _isEnabled = false;
        _canvasGroup.interactable = false;
        
        // Disable the colors
        Color c = Color.HSVToRGB(0, 0, _disabledHSV, true);
        _backImage.color = c;
        _charImage.color = c;
    }

    private void Update()
    {
        if (!_isEnabled) return;

        if (!_onDrag)
        {
            if (_onPointerDown && Time.time > _pointDownTimer + _mixHoldForInfo && !_infoVisible)
                ToggleCharacterInfo(true);
        }
    }

    /// <summary>
    /// Method which checks what kind of action the user will do based on where the panel will be dragged to
    /// </summary>
    public void OnDrag() 
    {
        if (!_isEnabled) return;

        // Enable the icons so we can show them if the user is dragging the panel
        if (!_onDrag)
        {
            _shieldIcon.gameObject.SetActive(true);
            _shieldIcon.alpha = 0;

            _specialIcon.gameObject.SetActive(true);
            _specialIcon.alpha = 0;
        }

        _onDrag = true;
        // Set the start position where we started the click
        if (_dragStartPos == Vector3.zero)
            _dragStartPos = Input.mousePosition;

        // Move the panel corresponding to the input position
        float mousePosY = Input.mousePosition.y - (_dragStartPos.y - _startY);
        _panel.anchoredPosition = new Vector3(0, mousePosY, 0);

        // Clamp the panel to the maxDrag
        if (_panel.anchoredPosition.y > _maxDrag + _startY)
        {
            _panel.anchoredPosition = new Vector3(0, _maxDrag + _startY, 0);
        }
        else if (_panel.anchoredPosition.y < -_maxDrag + _startY)
        {
            _panel.anchoredPosition = new Vector3(0, -_maxDrag + _startY, 0);
        }

        // Show the correct icon based on the drag direction
        _specialIcon.alpha = (_panel.anchoredPosition.y - _startY) / _maxDrag;
        _shieldIcon.alpha = (_panel.anchoredPosition.y - _startY) / -_maxDrag;

        // TODO: Smooth stop the panel instead of stopping it instantly
    }

    /// <summary>
    /// Method will start the action based on where the panel was dragged to
    /// </summary>
    public void OnEndDrag()
    {
        _onDrag = false;
        _dragStartPos = Vector2.zero;

        _specialIcon.gameObject.SetActive(false);
        if (_panel.anchoredPosition.y < _startY)
        {
            _shieldIcon.alpha = 1;
            UseAction(PlayerActions.Defend);
        }
        else if (_panel.anchoredPosition.y > _startY)
        {
            _shieldIcon.gameObject.SetActive(false);
            UseAction(PlayerActions.Special);
        }

        // Reset position
        _panel.anchoredPosition = new Vector3(0, _startY, 0);
    }

    public void OnPointerUp() 
    {
        _onPointerDown = false;

        if (_infoVisible)
            ToggleCharacterInfo(false);
        else if (_isEnabled && !_onDrag)
            UseAction(PlayerActions.Attack);
    }
    
    public void OnPointerDown() 
    {
        _onPointerDown = true;
        _pointDownTimer = Time.time;
    }

    private void UseAction(PlayerActions action)
    {
        ActionPerformed?.Invoke(action, this);
    }
    
    internal void EndAction()
    {
        DisablePanel();
    }

    private void ToggleCharacterInfo(bool show)
    {
        UseAction(PlayerActions.Info);
        _infoVisible = show;
    }
}
