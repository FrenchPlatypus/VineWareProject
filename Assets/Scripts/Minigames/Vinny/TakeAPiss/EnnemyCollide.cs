using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCollide : MonoBehaviour
{
    public TakeAPiss mainScript;

    public int hitPoints;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitPoints--;
        Destroy(collision.gameObject);

        if (hitPoints == 0)
        {
            Destroy(gameObject);
            mainScript.ennemyKills++;
        }
    }
}
