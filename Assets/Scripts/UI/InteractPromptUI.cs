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
            transform.rotation = _mainCam.transform.rotation;
        }
    }

    public void Show(string buttonPrompt)
    {
        if (promptCanvas == null)
        { 
            return;
        }

        if (promptText != null)
        {
            promptText.text = string.IsNullOrEmpty(buttonPrompt) ? "E" : buttonPrompt;
        }

        promptCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (promptCanvas != null)
        {
            promptCanvas.gameObject.SetActive(false);
        }
    }

    public void SetIcon(Sprite sprite)
    {
        if (promptImage != null)
        {
            promptImage.sprite = sprite;
        }

    }
}
