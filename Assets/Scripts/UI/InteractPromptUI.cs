using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InteractPromptUI : MonoBehaviour
{
    public Canvas promptCanvas;
    public Image promptImage;
    public TextMeshProUGUI promptText;
    public Vector3 worldOffset = new Vector3(0, 2, 0);
    public bool billboard = true;


    private Camera _mainCam;
    private InputAction _bindingAction;

    private void Awake()
    {
        _mainCam = Camera.main;

        if(promptCanvas != null)
        {
            promptCanvas.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if(promptCanvas == null || !promptCanvas.gameObject.activeSelf)
        {
            return;
        }

        transform.position = transform.parent.position + worldOffset;

        if(billboard && _mainCam != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _mainCam.transform.position);
        }
    }




    public void Show(InputAction action)
    {
        if(promptCanvas == null) { return; }

        _bindingAction = action;
        RefreshBindText();

        promptCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (promptCanvas != null)
            promptCanvas.gameObject.SetActive(false);

        _bindingAction = null;
    }

    private void RefreshBindText()
    {
        if (promptText == null || _bindingAction == null)
            return;

        string displayString = _bindingAction.GetBindingDisplayString(
            InputBinding.DisplayStringOptions.DontIncludeInteractions
        );

        promptText.text = string.IsNullOrEmpty(displayString) ? "E" : displayString;
    }

    public void SetIcon(Sprite sprite)
    {
        if(promptImage != null)
        {
            promptImage.sprite = sprite;
        }
    }
}
