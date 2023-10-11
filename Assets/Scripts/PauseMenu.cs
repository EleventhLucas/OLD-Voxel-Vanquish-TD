using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused;
    public NodeUI nodeUI;

    public GameObject ui;
    public GameObject PauseButton;
    public BannerAd bannerAd;

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
        if (!isPaused)
        {
            bannerAd.HideBanner();
        }
    }

    public void Toggle()
    {
        isPaused = true;
        ui.SetActive(!ui.activeSelf);
        PauseButton.SetActive(false);
        if (GameManager.GameIsOver)
        {
            PauseButton.SetActive(false);
            return;
        }

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            bannerAd.ShowBanner();
        } else
        {
            Time.timeScale = 1f;
            PauseButton.SetActive(true);
            bannerAd.HideBanner();
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
