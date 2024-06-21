using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light mainLight;
    public Light additionalLight;
    public float defaultIntensity = 1.0f;

    void Start()
    {
        // Загружаем сохраненные настройки освещения при инициализации сцены
        LoadSettings();
    }

    public void SetLightIntensity(float intensity)
    {
        mainLight.intensity = intensity;
        additionalLight.intensity = intensity;

        // Сохраняем настройки освещения
        SaveSettings(intensity);
    }

    public void SaveSettings(float intensity)
    {
        PlayerPrefs.SetFloat("LightIntensity", intensity);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("LightIntensity"))
        {
            float savedIntensity = PlayerPrefs.GetFloat("LightIntensity");
            mainLight.intensity = savedIntensity;
            additionalLight.intensity = savedIntensity;
        }
        else
        {
            mainLight.intensity = defaultIntensity;
            additionalLight.intensity = defaultIntensity;
        }
    }
}
