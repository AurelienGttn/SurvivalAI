using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

    private AudioSource mainMusic;
    [SerializeField] private Slider volumeSlider;

	void Start () {
        mainMusic = GetComponent<AudioSource>();
        volumeSlider.value = PlayerPrefs.GetFloat("mainVolume", 0.75f);
        mainMusic.volume = volumeSlider.value;
	}

    public void ChangeVolume(float sliderValue)
    {
        mainMusic.volume = sliderValue;
        PlayerPrefs.SetFloat("mainVolume", sliderValue);
    }
}
