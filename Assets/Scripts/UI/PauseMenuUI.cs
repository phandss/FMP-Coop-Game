using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;

    public void ShowPauseMenu()
    {
        if (_pauseMenuPanel != null)
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void HidePauseMenu()
    {
        if (_pauseMenuPanel != null)
        {
            _pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
