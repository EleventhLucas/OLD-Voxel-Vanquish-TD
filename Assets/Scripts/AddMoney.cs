using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoney : MonoBehaviour {

    public void Toggle()
    {
        PlayerStats.Money += 1000;
        Debug.Log("Added 1k cash.");
    }

}
