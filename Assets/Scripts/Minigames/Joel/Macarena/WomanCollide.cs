using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanCollide : MonoBehaviour
{
    public Macarena macarenaScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        macarenaScript.FailMinigame();
    }

}
