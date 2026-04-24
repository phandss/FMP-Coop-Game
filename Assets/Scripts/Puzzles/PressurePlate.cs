using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private TrapBase _trap;
    [SerializeField] private float _reactivationDelay = 1f;
    //[SerializeField] private Light[] _activationLights;
    [SerializeField] private Light _activationLight;

    [Header("Activation Settings")]
    [SerializeField] private Color _activationColour = Color.red;
    [SerializeField] private float _activationIntensity = 100f;
    [SerializeField] private AudioClip _activationSound;
    [SerializeField] private float _pressDepth;
    [SerializeField] private float _pressSpeed = 5f;

    private AudioSource _audioSource;
    private bool _isActivated = false;
    private Coroutine _reactivationCoroutine;
    //private Coroutine _pressCoroutine;
    //private Vector3 _initialPosition;
    //private Vector3 _pressedPosition;


    private void Awake()
    {

        _activationLight = GetComponentInChildren<Light>();
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        if(_activationLight != null)
        {
            _activationLight = gameObject.GetComponentInChildren<Light>();
        }

        //_initialPosition = transform.position;
        //_pressedPosition = transform.position - Vector3.up * _pressDepth;
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

        if (_activationLight != null)
        {
            _activationLight.intensity = _activationIntensity;
            _activationLight.color = _activationColour;
        }

        //Debug.Log($"Pressure plate activated by {other.name}");
        _isActivated = true;
        _trap.Activate();


        if (_trap.canReactivate)
        {
            _reactivationCoroutine = StartCoroutine(ReactivateAfterDelay());
        }

        //StartCoroutine(PressDown());

    }


    private void OnTriggerExit(Collider other)
    {
        if(!_isActivated)
        {
            return;
        }

        _isActivated = false;

        //if (_activationLight != null)
        //{
        //    _activationLight.intensity = 0;
        //}

        if (_reactivationCoroutine != null)
        {
            StopCoroutine(_reactivationCoroutine);
            _reactivationCoroutine = null;
        }

        //StartCoroutine(PressUp());

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

    //private IEnumerator PressDown()
    //{
    //    float elapsed = 0f;
    //    while (elapsed < _pressSpeed)
    //    {
    //        transform.position = Vector3.Lerp(_initialPosition, _pressedPosition, elapsed / _pressSpeed);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = _pressedPosition;
    //}

    //private IEnumerator PressUp()
    //{

    //    float elapsed = 0f;
    //    while (elapsed < _pressSpeed)
    //    {
    //        transform.position = Vector3.Lerp(_pressedPosition, _initialPosition, elapsed / _pressSpeed);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = _initialPosition;
    //}
}
