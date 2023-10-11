﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Leave empty to look at main camera")]
    private Camera Target = null;

    private Vector3 TargetPosition;

    void Start()
    {
        if (Target == null)
        {
            Target = Camera.main;
        }
    }

    private void LateUpdate()
    {
        //if (TargetPosition != Target.transform.position)
        //{
            TargetPosition = Target.transform.position;
            transform.LookAt(TargetPosition);
        //}
    }
}
