using System;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _lifetime = 5f;


    private Rigidbody rb;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

    }

    public void Fire(Vector3 direction, float speed)
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        rb.linearVelocity = direction.normalized * speed;      
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
        {
            return;
        }

        HumanHealth health = other.GetComponent<HumanHealth>();

        if(health != null)
        {
            health.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

}
