using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollide : MonoBehaviour
{
    public KillEm gameScript;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Oh no");
        gameScript.earthDamage();
    }

}
