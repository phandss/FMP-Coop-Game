using UnityEngine;

public class SpikeBallDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float _maxLifeSpan;


    private void Awake()
    {
        Destroy(gameObject, _maxLifeSpan);
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
