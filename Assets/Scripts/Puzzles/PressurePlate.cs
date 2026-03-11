using System;
using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private TrapBase _trap;
    [SerializeField] private float _reactivationDelay = 1f;

    private bool _isActivated = false;
    private Coroutine _reactivationCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated)
        {
            return;
        }

        _isActivated = true;
        _trap.Activate();
        Debug.Log($"Pressure plate activated by {other.name}");

        if (_trap.canReactivate)
        {
            _reactivationCoroutine = StartCoroutine(ReactivateAfterDelay());
        }

        _trap.Deactivate();
    }


    private void OnTriggerExit(Collider other)
    {
        if(!_isActivated)
        {
            return;
        }

        _isActivated = false;

        if(_reactivationCoroutine != null)
        {
            StopCoroutine(_reactivationCoroutine);
            _reactivationCoroutine = null;
        }

        _trap.Deactivate();
    }
    
    private IEnumerator ReactivateAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(_reactivationDelay);
            _isActivated = false;
        }
    }
}
