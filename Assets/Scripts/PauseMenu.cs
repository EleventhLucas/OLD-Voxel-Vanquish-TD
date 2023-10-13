using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused;
    public NodeUI nodeUI;

    [FormerlySerializedAs("ui")] public GameObject UI;
    public GameObject PauseButton;
    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
        if (GameManager.GameIsOver)
        {
            PauseButton.SetActive(false);
        }
        if (isPaused)
        {
            nodeUI.Hide();
        }
    }

    public void Toggle()
    {
        isPaused = true;
        UI.SetActive(!UI.activeSelf);
        PauseButton.SetActive(false);
        if (GameManager.GameIsOver)
        {
            PauseButton.SetActive(false);
            return;
        }

        if (UI.activeSelf)
        {
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = 1f;
            PauseButton.SetActive(true);
            isPaused = false;
        }
    }

    public void Retry ()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        PlayerStats.Rounds = 0;
    }

    public void Menu ()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
