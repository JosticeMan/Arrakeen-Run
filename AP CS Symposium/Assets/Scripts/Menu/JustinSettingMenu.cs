using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/**
 * Justin Yau
 * */
public class JustinSettingMenu : MonoBehaviour {

    public AudioMixer mixer;
    
    public bool default1;

    void Start()
    {
        if(default1) {
            setPreviousVolume();
        }
        if (PlayerPrefs.HasKey("volume"))
        {
            float vol = PlayerPrefs.GetFloat("volume");
            Slider s = GetComponentInChildren<Slider>();
            if (s != null)
            {
                mixer.SetFloat("volume", vol);
                s.value = vol;
            }
        }
    }

    public void setVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void setPreviousVolume()
    {
        if(PlayerPrefs.HasKey("volume"))
        {
            mixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
        }
    }

}
