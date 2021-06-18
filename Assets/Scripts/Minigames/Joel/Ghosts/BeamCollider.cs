using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollider : MonoBehaviour
{
    public Ghosts ghostScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        ghostScript.killCount++;
    }
}
