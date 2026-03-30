using UnityEngine;

public class VoidDamage : MonoBehaviour
{
    [SerializeField] private AudioClip _fallSound;
    
    private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<HumanHealth>().TakeDamage(9999);

            if(_fallSound != null)
            {
                if(_audioSource == null)
                {
                    _audioSource = gameObject.AddComponent<AudioSource>();
                }
                _audioSource.PlayOneShot(_fallSound);
            }
        }
    }
}
