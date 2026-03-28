using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private TrapBase _trap;
    [SerializeField] private float _reactivationDelay = 1f;
    [SerializeField] private Light[] _activationLights;

    [SerializeField] private Color _activationColour = Color.red;
    [SerializeField] private float _activationIntensity = 100f;
    [SerializeField] private AudioClip _activationSound;


    private AudioSource _audioSource;
    private bool _isActivated = false;
    private Coroutine _reactivationCoroutine;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
        if (_isActivated)
        {
            return;
        }

        if(_activationSound != null)
        {
            _audioSource.PlayOneShot(_activationSound);
        }

        foreach(var light in _activationLights)
        {
            light.intensity = _activationIntensity;
            light.color = _activationColour;
        }

        //Debug.Log($"Pressure plate activated by {other.name}");
        _isActivated = true;
        _trap.Activate();


        if (_trap.canReactivate)
        {
            _reactivationCoroutine = StartCoroutine(ReactivateAfterDelay());
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if(!_isActivated)
        {
            return;
        }

        _isActivated = false;

        foreach(var light in _activationLights)
        {
            light.intensity = 0;
        }

        if (_reactivationCoroutine != null)
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
