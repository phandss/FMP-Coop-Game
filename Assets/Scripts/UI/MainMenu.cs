using Unity.VisualScripting;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject _controlsMenu;

    void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ControlsButton()
    {
        _controlsMenu.SetActive(true);
    }

    void QuitButton()
    {
        Application.Quit();
    }

    void ReturnButton()
    {
        _controlsMenu.SetActive(false);
    }
}
