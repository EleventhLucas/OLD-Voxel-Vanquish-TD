using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint artillery;
    public TurretBlueprint zeus;
    public TurretBlueprint ice;
    public TurretBlueprint mage;
    public TurretBlueprint poison;
    public GameObject shopui;
    public GameObject shopClose;
    public GameObject shopOpen;


    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret ()
    {
        Debug.Log("Archer (Standard Turret) Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectArtillery ()
    {
        Debug.Log("Artillery Selected");
        buildManager.SelectTurretToBuild(artillery);
    }

    public void SelectZeus()
    {
        Debug.Log("Zeus Selected");
        buildManager.SelectTurretToBuild(zeus);
    }

    public void SelectIce()
    {
        Debug.Log("Ice Selected");
        buildManager.SelectTurretToBuild(ice);
    }

    public void SelectMage()
    {
        Debug.Log("Mage Selected");
        buildManager.SelectTurretToBuild(mage);
    }

    public void SelectPoison()
    {
        Debug.Log("Poison");
        buildManager.SelectTurretToBuild(poison);
    }

    public void HideShop()
    {
        shopui.SetActive(false);
        shopOpen.SetActive(true);
        shopClose.SetActive(false);
    }

    public void ShowShop()
    {
        shopui.SetActive(true);
        shopClose.SetActive(true);
        shopOpen.SetActive(false);
    }

}
