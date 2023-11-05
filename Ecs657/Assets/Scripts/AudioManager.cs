using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public const string master = "MasterVolume";
    public static AudioManager instance;

    // Start is called before the first frame update
    //Singleton pattern
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    //Set volume to x
    void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat(master,0f);
        mixer.SetFloat(master,volume);
    }
}
