using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelInput
{
    Click,
    SwipeUp,
    SwipeDown,
    Hold
}

public class CharacterPanel : MonoBehaviour
{
    private bool _isEnabled = true;
    private float _startY = 0;

    [Header("Panel components")]
    [SerializeField] private RectTransform _panel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _backImage, _charImage;
    [SerializeField] private float _enabledHSV, _disabledHSV = 1;
    [SerializeField] private CanvasGroup _swipeDownIcon, _swipeUpIcon;

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

    internal void EnablePanel()
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
            _swipeDownIcon.gameObject.SetActive(true);
            _swipeDownIcon.alpha = 0;

            _swipeUpIcon.gameObject.SetActive(true);
            _swipeUpIcon.alpha = 0;
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
        _swipeUpIcon.alpha = (_panel.anchoredPosition.y - _startY) / _maxDrag;
        _swipeDownIcon.alpha = (_panel.anchoredPosition.y - _startY) / -_maxDrag;

        // TODO: Smooth stop the panel instead of stopping it instantly
    }

    /// <summary>
    /// Method will start the action based on where the panel was dragged to
    /// </summary>
    public void OnEndDrag()
    {
        _onDrag = false;
        _dragStartPos = Vector2.zero;

        _swipeUpIcon.gameObject.SetActive(false);
        if (_panel.anchoredPosition.y < _startY)
        {
            _swipeDownIcon.alpha = 1;
            UseAction(PanelInput.SwipeDown);
        }
        else if (_panel.anchoredPosition.y > _startY)
        {
            _swipeDownIcon.gameObject.SetActive(false);
            UseAction(PanelInput.SwipeUp);
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
            UseAction(PanelInput.Click);
    }
    
    public void OnPointerDown() 
    {
        _onPointerDown = true;
        _pointDownTimer = Time.time;
    }

    private void UseAction(PanelInput action) { SignalBus.Broadcast(new EvPanelAction(action, this)); }
    
    internal void EndAction()
    {
        DisablePanel();
    }

    private void ToggleCharacterInfo(bool show)
    {
        UseAction(PanelInput.Hold);
        _infoVisible = show;
    }
}
