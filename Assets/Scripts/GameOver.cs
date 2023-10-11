using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    public void Retry ()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // You can use a number index too such as SceneManager.LoadScene(0) - You can see this in the build settings. The "SampleScene" which we can also use, is index 0.

    }

    public void Menu ()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}
