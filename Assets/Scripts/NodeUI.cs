using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;
    private int cost;

    private Node target;
    public GameObject rangeParticles;
    public GameObject selectionParticles;

    [HideInInspector]
    public GameObject effect; //These effect values are hidden and just used publicly in the script.
    [HideInInspector]
    public GameObject effect2;

	public void SetTarget (Node _target)
    {
        Hide();
        target = _target;

        transform.position = target.GetBuildPosition();

        if (target.upgradeStage == 0)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            if (PlayerStats.Money >= target.turretBlueprint.upgradeCost)
            {
                upgradeButton.interactable = true;
            } else
                upgradeButton.interactable = false;
        }
        else if (target.upgradeStage == 1)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost2;
            if (PlayerStats.Money >= target.turretBlueprint.upgradeCost2)
            {
                upgradeButton.interactable = true;
            }
            else
                upgradeButton.interactable = false;
        }
        else if (target.upgradeStage == 2)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost3;
            if (PlayerStats.Money >= target.turretBlueprint.upgradeCost3)
            {
                upgradeButton.interactable = true;
            }
            else
                upgradeButton.interactable = false;
        }
        else if (target.upgradeStage == 3)
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.GetSellAmount();

        ui.SetActive(true);

        //Implementing the range
        float range = TowerRange(target.turret);
        effect = (GameObject)Instantiate(rangeParticles, target.GetBuildPosition() + new Vector3(0,0.5f,0), Quaternion.identity);
        effect.transform.localScale = new Vector3(range, range, range);
        effect2 = (GameObject)Instantiate(selectionParticles, target.GetBuildPosition(), Quaternion.identity); //If using 5x5 towers, they will need a new effect.

    }

    // Fuck this function btw
    public float TowerRange (GameObject tower)
    {
        try
        {
            return tower.GetComponent<Turret>().range;
        }
        catch (Exception)
        {
            try
            {
                return tower.GetComponent<TurretAOE>().range;
            }
            catch (Exception)
            {
                try
                {
                    return tower.GetComponent<TurretDOT>().range;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }

    public void Hide ()
    {
        Destroy(effect, 0f);
        Destroy(effect2, 0f);
        ui.SetActive(false);
    }

    public void Upgrade ()
    {
        FindObjectOfType<AudioManager>().Play("TowerUpgrade");
        if (target.upgradeStage == 0)
        {
            target.UpgradeTurret();
        }
        else if (target.upgradeStage == 1)
        {
            target.UpgradeTurret2();
        }
        else if (target.upgradeStage == 2)
        {
            target.UpgradeTurret3();
        }

        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
