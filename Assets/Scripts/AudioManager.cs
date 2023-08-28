using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public static AudioManager instance;

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SfxVolume";



    private void Awake()
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

    // Volume saved in volumeSetting.cs
    void LoadVolume()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(volumeSetting.MIXER_MUSIC, Mathf.Log10(musicVol) * 20);
        mixer.SetFloat(volumeSetting.MIXER_SFX, Mathf.Log10(sfxVol) * 20);

    }
}
