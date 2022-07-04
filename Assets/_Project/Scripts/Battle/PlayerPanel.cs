using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private bool _isEnabled = true;
    private float _startY = 0;

    [Header("Panel components")]
    [SerializeField] private RectTransform _panel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _charImage;

    [Header("Icons")]
    [SerializeField] private CanvasGroup _shieldIcon;
    [SerializeField] private CanvasGroup _specialIcon;

    [Header("Character")]
    private bool _infoVisible;

    [Header("Input Actions")]
    [SerializeField] private float _maxDrag = 20;
    private Vector3 _dragStartPos;
    private bool _onDrag;

    private bool _onPointerDown;
    private float _pointDownTimer;
    [SerializeField] private float _mixHoldForInfo = 1;
    [SerializeField] private float _maxHoldForAttack = 0.1f;

    /// <summary>
    /// Method it initialize the panel
    /// </summary>
    internal void SetPosition(float startY)
    {
        _startY = startY;
        _panel.anchoredPosition = new Vector3(0, _startY, 0);
    }

    /// <summary>
    /// Method to set the image cover for the panel
    /// </summary>
    internal void SetImageCover(Sprite spr) { _charImage.sprite = spr; }


    /// <summary>
    /// Method to set the background tint color
    /// </summary>
    internal void SetBackgroundTint(Color c) { _backImage.color = c; }

    private void EnablePanel()
    {
        _isEnabled = true;
        _canvasGroup.interactable = true;

        // Reset the colors
        Color c = Color.HSVToRGB(0, 0, 1, true);
        _backImage.color = c;
        _charImage.color = c;
    }

    private void DisablePanel()
    {
        _isEnabled = false;
        _canvasGroup.interactable = false;
        
        // Disable the colors
        Color c = Color.HSVToRGB(0, 0, 0.25f, true);
        _backImage.color = c;
        _charImage.color = c;
    }

    private void Update()
    {
        if (!_isEnabled) return;

        if (!_onDrag)
        {
            if (_onPointerDown && Time.time > _pointDownTimer + _mixHoldForInfo && !_infoVisible)
                ShowCharacterInfo();
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
            UseDefend();
        }
        else if (_panel.anchoredPosition.y > _startY)
        {
            _shieldIcon.gameObject.SetActive(false);
            UseSpecial();
        }

        // Reset position
        _panel.anchoredPosition = new Vector3(0, _startY, 0);
    }

    public void OnPointerUp() 
    {
        _onPointerDown = false;

        if (_infoVisible)
            HideCharacterInfo();
        else if (_isEnabled && !_onDrag)
            UseAttack();
    }
    
    public void OnPointerDown() 
    {
        _onPointerDown = true;
        _pointDownTimer = Time.time;
    }

    private void UseAttack()
    {
        Debug.Log("Attack");

        // TODO: Wait until action has been completed
        EndAction();
    }

    private void UseDefend()
    {
        Debug.Log("Defend");

        // TODO: Wait until action has been completed
        EndAction();
    }

    private void UseSpecial()
    {
        Debug.Log("Special");

        // TODO: Wait until action has been completed
        EndAction();
    }
    
    private void EndAction()
    {
        DisablePanel();
    }

    private void ShowCharacterInfo()
    {
        Debug.Log("ShowCharacterInfo");
        _infoVisible = true;
    }

    private void HideCharacterInfo()
    {
        Debug.Log("HideCharacterInfo");
        _infoVisible = false;
    }
}
