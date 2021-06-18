using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonCollide : MonoBehaviour
{
    public Racoon minigameScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pouet");
        if (minigameScript.fail == false)
        {
            minigameScript.fail = true;

        }
    }
}
