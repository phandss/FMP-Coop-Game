using Unity.VisualScripting;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float _maxLifeSpan;
    private float _currentLifeSpan;


    private void Awake()
    {
        _currentLifeSpan = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        HumanHealth _health = other.GetComponent<HumanHealth>();

        if(_health != null)
        {
            _health.TakeDamage(damage);
        }

        if(_maxLifeSpan <= _currentLifeSpan)
        {
            Destroy(gameObject);
        }


    }
}
