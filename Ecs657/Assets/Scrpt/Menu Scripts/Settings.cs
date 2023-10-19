using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    string master = "MasterVolume";

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.master,0f);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currrentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currrentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currrentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void OnDisable(){
        PlayerPrefs.SetFloat(AudioManager.master,masterSlider.value);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(master, volume);
    }

    public void SetFullscreen( bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution( int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
