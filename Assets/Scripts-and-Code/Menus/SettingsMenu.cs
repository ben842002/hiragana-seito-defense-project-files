using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Fullscreen")]
    public Toggle fullscreenToggle;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider slider;

    [Header("Resolution")]
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        CalculateScreenResolutions();

        // -----------------------------------------------
        // load settings data and adjust UI to match values
        LoadFullscreenDataForToggle();
        LoadSliderAndVolumeData();
    }

    void CalculateScreenResolutions()
    {
        // store resolutions in a variable
        resolutions = Screen.resolutions.Select(resolution =>
            new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); // Show the resolution value UI.
    }

    void LoadFullscreenDataForToggle()
    {
        // fullscreen data
        int fullScreen = PlayerPrefs.GetInt("fullscreen", 1);
        if (fullScreen == 1)
            fullscreenToggle.isOn = true;
        else
            fullscreenToggle.isOn = false;
    }

    void LoadSliderAndVolumeData()
    {
        // load slider value saved from SetVolume(volume)
        // if no value is found, default is 0.5f (second parameter)
        slider.value = PlayerPrefs.GetFloat("VolumeSliderValue", 0.5f);

        // make sure the volume level matches the slider value
        // we don't want scenarios where slider value is 1f, but the volume level is at 0.5f;
        audioMixer.SetFloat("Volume", Mathf.Log10(slider.value) * 20);
    }

    // ----------------------------------------------------
    // Functions below and called on OnClick UI in scene

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        // name of the parameter. Audiomixer volume IS NOT linear
        // convert slider value to audioMixer value
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

        // save the volume slider 
        PlayerPrefs.SetFloat("VolumeSliderValue", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        // save the toggle status 
        int saveBool;
        if (isFullscreen == true)
            saveBool = 1;
        else
            saveBool = 0;

        PlayerPrefs.SetInt("fullscreen", saveBool);
    }
}
