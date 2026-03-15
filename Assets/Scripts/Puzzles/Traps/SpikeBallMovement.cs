using UnityEngine;
using System.Collections;

public class SpikeBallMovement : MonoBehaviour
{
    [SerializeField] private float _forceStrength = 5f;

    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 10f;
        StartCoroutine(ChangeRotation());
    }

    private IEnumerator ChangeRotation()
    {
        while (true)
        {
            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rb.AddForce(randomDir * _forceStrength, ForceMode.Impulse);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
