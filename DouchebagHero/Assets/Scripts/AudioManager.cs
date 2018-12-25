using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public AudioMixer mixer;
    public AudioSetting[] audioSettings;
    private AudioSource sfxSample;
    private enum AudioGroups { Music, SFX };

    void Awake()
    {
        instance = GetComponent<AudioManager>();
        sfxSample = GetComponent<AudioSource>();
    }

    private void Start()
    {
        for (int i = 0; i < audioSettings.Length; i++)
        {
            audioSettings[i].Initialize();
        }
    }

    public void SetMusicVolume(float value)
    {
        audioSettings[(int)AudioGroups.Music].SetExposedParam(value);
    }

    public void SetSFXVolume(float value)
    {
        audioSettings[(int)AudioGroups.SFX].SetExposedParam(value);
        if (!sfxSample.isPlaying)
            sfxSample.Play();
    }

    [System.Serializable]
    public class AudioSetting
    {
        public Slider slider;
        public string exposedParam;

        public void Initialize()
        {
            slider.value = PlayerPrefs.GetFloat(exposedParam, 0.75f);
        }

        public void SetExposedParam(float value)
        {
            instance.mixer.SetFloat(exposedParam, Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(exposedParam, value);
        }
    }
}