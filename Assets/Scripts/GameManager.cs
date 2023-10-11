using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public bool isFastForward = false;
    public GameObject fastForwardOnUI;
    public GameObject fastForwardOffUI;

    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public NodeUI nodeUI;

    private void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    private void Update () 
    {
        if (GameManager.GameIsOver)
        {
            fastForwardOnUI.SetActive(false);
            fastForwardOffUI.SetActive(false);
            this.enabled = false;
            return;
        }

        if (!PauseMenu.isPaused && isFastForward)
        {
            Time.timeScale = 2;
        } 
        else if (PauseMenu.isPaused)
        {
            Time.timeScale = 0;
        } 
        else
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

	    if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
	}

    public void FastForward()
    {
        fastForwardOffUI.SetActive(false);
        fastForwardOnUI.SetActive(true);
        isFastForward = true;
    }

    public void FastForwardOff()
    {
        fastForwardOnUI.SetActive(false);
        fastForwardOffUI.SetActive(true);
        isFastForward = false;
    }

    private void EndGame ()
    {
        nodeUI.Hide();
        GameIsOver = true;
        gameOverUI.SetActive(true);
        isFastForward = false;
    }

    public void WinLevel()
    {
        Time.timeScale = 1;
        isFastForward = false;
        nodeUI.Hide();
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
