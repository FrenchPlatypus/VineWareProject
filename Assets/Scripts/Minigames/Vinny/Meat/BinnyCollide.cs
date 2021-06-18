using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinnyCollide : MonoBehaviour
{
    public Meat meatMinigame;
    public bool collisionOn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collisionOn == false)
        {
            if (collision.gameObject.name == "Meat")
            {
                collisionOn = true;
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Animator>().Play("BinnyDeath");

                meatMinigame.BinnyDeath();
            }

            if(collision.gameObject.name == "MeatHead")
            {
                collisionOn = true;
                meatMinigame.BinnyWin();
            }
        }

        if (collision.gameObject.name == "Ground")
        {
            meatMinigame.airBorne = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionOn == false)
        {
            if (collision.gameObject.name == "Meaties(Clone)")
            {
                Debug.Log("collide with little meat");
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Animator>().Play("BinnyDeath");

                collisionOn = true;
                meatMinigame.BinnyDeath();
            }
        }
    }
}
