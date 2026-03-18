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
        Debug.Log("Arrow hit: " + other.name);
        HumanHealth health = other.GetComponentInParent<HumanHealth>();

        if (health != null)
        {
            health.TakeDamage(_damage);
        }

        if (hasHit)
        {
            return;
        }

        

        Destroy(gameObject);
    }

}
