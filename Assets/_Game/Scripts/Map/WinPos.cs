using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] private Transform top1Pos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
            
            character.OnWin();
            StartCoroutine(character.MovePosition(top1Pos.position, 0.5f));
        }
    }
}
