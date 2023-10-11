using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoneyColor;

    //This is the offset variable for when the turret is built. Puts it above the pedestal.
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    public int upgradeStage = 0;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    /*                              Hidden for later PC release.
    void OnMouseUpAsButton ()
    {
        BuildTurret();
    }
    */

    public void OnMouseDown ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build! (Make an announcement later)");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        FindObjectOfType<AudioManager>().Play("TowerPlace");

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret() // This is simply the BuildTurret function but changed slightly
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade! (Make an announcement later)");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);

        //Build the new fancy one :)
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        upgradeStage++;

        Debug.Log("Turret Upgraded!");
    }

    public void UpgradeTurret2()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost2)
        {
            Debug.Log("Not enough money to upgrade 2! (Make an announcement later)");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost2;

        //Get rid of the old turret
        Destroy(turret);

        //Build the new fancy one :)
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab2, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        upgradeStage++;

        Debug.Log("Turret Upgraded 2!");
    }

    public void UpgradeTurret3()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost3)
        {
            Debug.Log("Not enough money to upgrade 3! (Make an announcement later)");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost3;

        //Get rid of the old turret
        Destroy(turret);

        //Build the new fancy one :)
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab3, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        upgradeStage++;

        Debug.Log("Turret Upgraded 3!");
    }

    public int GetSellAmount()
    {

        if (upgradeStage == 1)
        {
            return (int) ((turretBlueprint.upgradeCost + turretBlueprint.cost) * 0.6f);
        }
        else if (upgradeStage == 2)
        {
            return (int) ((turretBlueprint.upgradeCost + turretBlueprint.upgradeCost2 + turretBlueprint.cost) * 0.6f);
        }
        else if (upgradeStage == 3)
        {
            return (int) ((turretBlueprint.upgradeCost + turretBlueprint.upgradeCost2 + turretBlueprint.upgradeCost3 + turretBlueprint.cost) * 0.6f);
        }

        return (int) ((turretBlueprint.cost) * 0.6f);
    }

    public void SellTurret()
    {
        PlayerStats.Money += GetSellAmount();

        //Spawn a cool effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        upgradeStage = 0;
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (CameraController.isPanning || PanZoom.isZooming)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
