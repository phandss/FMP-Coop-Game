using UnityEngine;
using UnityEngine.SceneManagement;

public class VoidDamage : MonoBehaviour
{

    [SerializeField] private AudioClip _fallSound;
    [SerializeField] private GameObject _movingPlatform;
    private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {


            if (_fallSound != null)
            {
                if(_audioSource == null)
                {
                    _audioSource = gameObject.AddComponent<AudioSource>();
                }
                _audioSource.PlayOneShot(_fallSound);
            }

            if(_movingPlatform != null)
            {
                _movingPlatform.GetComponent<MovingPlatform>().PlatformReset();
            }


            UniqueKillPlayer();
        }
    }

    private void UniqueKillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
