using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public Text soundText;
    // Update is called once per frame
    void Update()
    {
        if (AudioListener.volume < 1)
        {
            soundText.text = "Sound Off" ;
        } else
        {
            soundText.text = "Sound On";
        }
    }
    public void ChangeVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Muted", 1);
            return;
        }
        else if (PlayerPrefs.GetInt("Muted") == 1)
        {
            PlayerPrefs.SetInt("Muted", 0);
            AudioListener.volume = 1;
        }
    }
}
