using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleSlide : MonoBehaviour {

    public float angle = -30f;

    // Update is called once per frame
    void Update() {
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }
}
