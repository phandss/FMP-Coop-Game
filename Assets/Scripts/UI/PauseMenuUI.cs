using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;

    public void ShowPauseMenu()
    {
        if (_pauseMenuPanel != null)
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }

    public void HidePauseMenu()
    {
        if (_pauseMenuPanel != null)
        {
            _pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f; // Resume the game
        }
    }
}
