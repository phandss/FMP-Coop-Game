using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _endGameUI;

    private void Start()
    {
        _endGameUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _endGameUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }
}
