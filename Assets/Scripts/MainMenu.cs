using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;

    public void Play ()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit ()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Contact()
    {
        Application.OpenURL("https://twitter.com/jazzapps");
        Debug.Log("Contact");
    }
}
