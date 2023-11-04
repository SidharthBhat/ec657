using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public const string master = "MasterVolume";
    public static AudioManager instance;
    // Start is called before the first frame update
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

    

    void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat(master,0f);
        mixer.SetFloat(master,volume);
    }
}
