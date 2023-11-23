using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GateOut : MonoBehaviour
{
    [SerializeField] private int stageId;
    public int StageId => stageId;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            
        }
    }
}
