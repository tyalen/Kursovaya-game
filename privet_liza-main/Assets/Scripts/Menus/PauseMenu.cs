using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        UIManager.instance.HidePaused();
    }

    public void OpenSettings()
    {
        UIManager.instance.ShowSettings("Pause menu");
    }

    public void QuitToMainMenu()
    {
        UIManager.instance.LoadLevel("Main menu");
    }
}
