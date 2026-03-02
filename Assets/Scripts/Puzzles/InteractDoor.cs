using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InteractDoor : InteractObjectBase
{
    [SerializeField] private Transform leftDoorPanel;
    [SerializeField] private Transform rightDoorPanel;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openTimer = .8f;

    public override bool isInteractable => !_isAnimating && !_isLocked;
    public override bool isDraggable => false;

    private bool _isAnimating;
    private bool _isOpen;
    private bool _isLocked;

    private Quaternion _leftClosed;
    private Quaternion _rightClosed;
    private Quaternion _leftOpen;
    private Quaternion _rightOpen;

    protected override void Awake()
    {
        base.Awake();
        _leftClosed = leftDoorPanel.localRotation;
        _rightClosed = rightDoorPanel.localRotation;
        _leftOpen = Quaternion.Euler(0, -openAngle, 0) * _leftClosed;
        _rightOpen = Quaternion.Euler(0, openAngle, 0) * _rightClosed;
    }

    public override void OnInteract() => StartCoroutine(AnimateDoor(isOpen : true));

    private IEnumerator AnimateDoor(bool isOpen, bool isLocked = false)
    {
        _isAnimating = true;
        Quaternion leftTarget = isOpen ? _leftOpen : _leftClosed;
        Quaternion rightTarget = isOpen ? _rightOpen : _rightClosed;
        float t = 0;

        while (t < openTimer)
        {
            t += Time.deltaTime;
            float eased = Mathf.SmoothStep(0, 1, t);
            leftDoorPanel.localRotation = Quaternion.Lerp(leftDoorPanel.localRotation, leftTarget, eased);
            rightDoorPanel.localRotation = Quaternion.Lerp(rightDoorPanel.localRotation, rightTarget, eased);
            yield return null;
        }

        leftDoorPanel.localRotation = leftTarget;
        rightDoorPanel.localRotation = rightTarget;
        _isOpen = isOpen;
        _isLocked = isLocked;
        _isAnimating = false;
    }

    public void OnLockTrigger()
    {
        if(_isOpen && !_isAnimating)
        {
            StartCoroutine(AnimateDoor(isOpen : false, isLocked : true));
        }
    }


    public override void OnHoverEnter(string buttonPrompt)
    {
        if(!_isLocked && !_isAnimating)
        {
            base.OnHoverEnter(buttonPrompt);
        }
        
    }

}
