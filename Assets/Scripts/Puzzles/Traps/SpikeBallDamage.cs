using UnityEngine;

public class SpikeBallDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float _maxLifeSpan;
    [SerializeField] bool hasLifeSpan = true;


    private void Awake()
    {
        if (hasLifeSpan)
        {
            Destroy(gameObject, _maxLifeSpan);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HumanHealth _health = other.GetComponent<HumanHealth>();

        if(_health != null)
        {
            _health.TakeDamage(damage);
        }

    }
}
