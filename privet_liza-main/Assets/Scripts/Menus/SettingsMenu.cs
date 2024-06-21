using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;
    public Text musicVolumeText;
    public Text effectVolumeText;
    public MusicManager musicManager;
    //public LightingManager lightingManager;

    private bool isFromPauseMenu = false;

    private void Start()
    {
        // Загрузка настроек
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float effectVolume = PlayerPrefs.GetFloat("EffectVolume", 0.5f);

        // Установка начальных значений слайдеров
        musicVolumeSlider.value = musicVolume * 100;
        effectVolumeSlider.value = effectVolume * 100;
        musicVolumeText.text = (musicVolume * 100).ToString("0");
        effectVolumeText.text = (effectVolume * 100).ToString("0");

        // Подписка на события изменения значения слайдеров
        musicVolumeSlider.onValueChanged.AddListener((v) =>
        {
            float normalizedVolume = v / 100f;
            SetMusicVolume(normalizedVolume);
            musicVolumeText.text = v.ToString("0");
        });

        effectVolumeSlider.onValueChanged.AddListener((v) =>
        {
            float normalizedVolume = v / 100f;
            SetEffectVolume(normalizedVolume);
            effectVolumeText.text = v.ToString("0");
        });

        // Установка начальной интенсивности освещения
        //float initialIntensity = 1.5f;
        //lightingManager.SetLightIntensity(initialIntensity);
    }

    public void SetMusicVolume(float volume)
    {
        musicManager.SetMusicVolume(volume);
    }

    public void SetEffectVolume(float volume)
    {
        musicManager.SetEffectVolume(volume);
    }

    //public void SetLightIntensity(float intensity)
    //{
    //    lightingManager.SetLightIntensity(intensity);
    //}

    //public void UpdateLightIntensity(float newIntensity)
    //{
    //    lightingManager.SetLightIntensity(newIntensity);
    //}

    public void BackButton()
    {
        string previousMenu = PlayerPrefs.GetString("PreviousMenu");
        if (previousMenu == "Pause menu")
        {
            UIManager.instance.CloseSettingsAndReturnToPauseMenu();
        }
        else if (previousMenu == "Main menu")
        {
            UIManager.instance.CloseSettingsAndReturnToMainMenu();
        }
    }

}
