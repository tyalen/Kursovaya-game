using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void ShowPaused()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Pause menu", LoadSceneMode.Additive);
    }

    public void HidePaused()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Pause menu");
    }

    public void ShowSettings(string previousMenu)
    {
        Time.timeScale = 0f; // Останавливаем время (если нужно для паузы)

        // Загружаем сцену настроек, передавая информацию о предыдущем меню
        SceneManager.LoadScene("Settings menu", LoadSceneMode.Additive);
        PlayerPrefs.SetString("PreviousMenu", previousMenu); // Сохраняем предыдущее меню в PlayerPrefs
    }

    public void CloseSettingsAndReturnToPauseMenu()
    {

        // Выгружаем сцену настроек
        SceneManager.UnloadSceneAsync("Settings menu");

        // Очищаем сохраненное предыдущее меню
        PlayerPrefs.DeleteKey("PreviousMenu");
    }

    public void CloseSettingsAndReturnToMainMenu()
    {

        SceneManager.UnloadSceneAsync("Settings menu");

        LoadLevel("Main menu");

        // Очищаем сохраненное предыдущее меню
        PlayerPrefs.DeleteKey("PreviousMenu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (SceneManager.GetSceneByName("Pause menu").isLoaded)
        {
            HidePaused();
        }
        else
        {
            ShowPaused();
        }
    }
}
