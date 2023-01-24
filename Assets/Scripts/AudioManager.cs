using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicValue"))
        {
            PlayerPrefs.SetFloat("musicValue", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }

    void Load()
    {
        slider.value = PlayerPrefs.GetFloat("musicValue");
    }

    void Save()
    {
        PlayerPrefs.SetFloat("musicValue", slider.value);
    }
}
