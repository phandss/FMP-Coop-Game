using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _endGameUI;


    [SerializeField] private AudioClip _endGameMusic1;
    [SerializeField] private AudioClip _endGameMusic2;
    [SerializeField] private AudioSource _ambientGameMusic;
    [SerializeField] private AudioSource _endGameMusicSource;


    private void Start()
    {
        _endGameUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _endGameUI.SetActive(true);
            Time.timeScale = 0f;

            //play end music
            _ambientGameMusic.Stop();
            _endGameMusicSource.PlayOneShot(_endGameMusic1);
            _endGameMusicSource.PlayOneShot(_endGameMusic2);
        }
    }
}
