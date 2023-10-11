using UnityEngine;

public class StartRound : MonoBehaviour {

    public GameObject ui;

    public void Toggle()
    {
        WaveSpawner.EnemiesAlive = 0;
        ui.SetActive(!ui.activeSelf);
    }
}
