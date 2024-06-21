using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public void continueGame()
    {

    }

    public void newGame()
    {
        UIManager.instance.LoadLevel("Level1");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void OpenSettingsFromMainMenu()
    {
        UIManager.instance.ShowSettings("Main menu");
    }
}
