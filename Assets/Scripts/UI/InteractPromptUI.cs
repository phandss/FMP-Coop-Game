using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InteractPromptUI : MonoBehaviour
{

    public static InteractPromptUI Instance { get; private set; }

    [SerializeField] private GameObject _promptObj;
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private Vector2 _offset = new Vector2(15, -15);

    private Transform _anchor;
    private Camera _mainCam;

    private void Awake()
    {
        Instance = this;
        _mainCam = Camera.main;

        if (_promptObj != null)
        {
            _promptObj.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if(!_promptObj.activeSelf || _promptObj == null || _anchor == null)
        {
            return;
        }

        Vector3 screenPos = _mainCam.WorldToScreenPoint(_anchor.position);

        if(screenPos.z < 0)
        {
            _promptObj.SetActive(false);
            return;
        }

        ((RectTransform)_promptObj.transform).anchoredPosition = new Vector2(screenPos.x, screenPos.y) + _offset;
    }

    public void Show(string buttonPrompt, Transform anchor)
    {
        if(_promptObj == null)
        {
            return;
        }

        _anchor = anchor;
        _promptText.text = buttonPrompt;
        _promptObj.SetActive(true);
    }

    public void Hide()
    {
        if(_promptObj == null)
        {
            return;
        }

        _promptObj.SetActive(false);
        _anchor = null;
    }

}
