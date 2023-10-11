using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint {
    
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public GameObject upgradedPrefab2;
    public int upgradeCost2;

    public GameObject upgradedPrefab3;
    public int upgradeCost3;

    private Node node;


    //Old Function lul
    /*
    public int GetSellAmount()
    {

        if (node.upgradeStage == 1)
        {
            cost = upgradeCost;
        }
        else if (node.upgradeStage == 2)
        {
            cost = upgradeCost2;
        }
        else if (node.upgradeStage == 3)
        {
            cost = upgradeCost3;
        }

        return cost / 2;
    }
    */
}
