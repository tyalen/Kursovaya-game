using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    private const string MusicVolumeKey = "MusicVolume";
    private const string EffectVolumeKey = "EffectVolume";

    private void Start()
    {
        LoadSettings();
    }

    public void SetMusicVolume(float volume)
    {
        float dbVolume = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20; 
        Debug.Log("Setting Music Volume to: " + dbVolume);
        audioMixer.SetFloat("MusicVolume", dbVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetEffectVolume(float volume)
    {
        float dbVolume = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20; 
        Debug.Log("Setting Effect Volume to: " + dbVolume);
        audioMixer.SetFloat("EffectVolume", dbVolume);
        PlayerPrefs.SetFloat(EffectVolumeKey, volume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            SetMusicVolume(musicVolume);
        }

        if (PlayerPrefs.HasKey(EffectVolumeKey))
        {
            float effectVolume = PlayerPrefs.GetFloat(EffectVolumeKey);
            SetEffectVolume(effectVolume);
        }
    }
}
