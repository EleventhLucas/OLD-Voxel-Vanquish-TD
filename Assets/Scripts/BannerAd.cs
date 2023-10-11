using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{

    string gameId = "3252251";
    private string banner_ad = "MenuBanner";
    public bool testMode = true;

    // Start is called before the first frame update
    void Start()
    {
        //Advertisement.Initialize (gameId, testMode);
        //Debug.Log("Unity Ads initialized: " + Advertisement.isInitialized);
        //Debug.Log("Unity Ads is supported: " + Advertisement.isSupported);
        //Debug.Log("Unity Ads in test mode: " + Advertisement.testMode);
    }

    public void ShowBanner()
    {
        //StartCoroutine(ShowBannerWhenReady());
        //Debug.Log("Unity Ads initialized on call: " + Advertisement.isInitialized);
    }

    public void HideBanner()
    {
        //Advertisement.Banner.Hide();
    }

    IEnumerator ShowBannerWhenReady ()
    {
        while (!Advertisement.IsReady(banner_ad))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(banner_ad);
    }
}
