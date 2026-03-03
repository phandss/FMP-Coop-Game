using Unity.VisualScripting;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject _controlsMenu;

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ControlsButton()
    {
        _controlsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReturnButton()
    {
        _controlsMenu.SetActive(false);
    }
}
