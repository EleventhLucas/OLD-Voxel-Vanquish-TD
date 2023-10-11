using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        return;
    }
}