using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] GameObject settingsMenu;

    Resolution[] resolutions;

    private void Start()
    {
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music Volume", volume);
    }
    public void SetSoundEffectVolume(float volume)
    {
        audioMixer.SetFloat("Sound Effects Volume", volume);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void closeSettings()
    {
        settingsMenu.SetActive(false);
    }
}
