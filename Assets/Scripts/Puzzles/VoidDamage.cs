using UnityEngine;

public class VoidDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<HumanHealth>().TakeDamage(9999);
        }
    }
}
