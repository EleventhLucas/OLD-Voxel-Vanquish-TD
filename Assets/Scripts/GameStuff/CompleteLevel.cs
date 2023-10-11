using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    public void Continue ()
    {
        UnlockNextLevel();
        sceneFader.FadeTo(nextLevel);
    }

	public void Menu ()
    {
        UnlockNextLevel();
        sceneFader.FadeTo(menuSceneName);
    }

    private void UnlockNextLevel ()
    {
        if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }
}
