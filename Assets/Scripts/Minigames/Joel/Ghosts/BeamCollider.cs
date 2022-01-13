using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollider : MonoBehaviour
{
    [SerializeField] private Ghosts ghostScript;

    [SerializeField] private int xLimit;
    [SerializeField] private int yLimit;

    [SerializeField] private float movingMultiplier;

    [SerializeField] private Transform[] ghosts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("pouet");
        collision.GetComponent<Animator>().Play("GhostDeath");
        collision.transform.parent.GetComponent<Animator>().enabled = false;
        ghostScript.killCount++;
    }

    private void Start()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            int[] xRand = new int[2];
            int[] yRand = new int[2];

            xRand[0] = Random.Range(-xLimit, 273);
            xRand[1] = Random.Range(-273, xLimit);

            yRand[0] = Random.Range(-yLimit, 195);
            yRand[1] = Random.Range(-195, yLimit);

            int xPos = xRand[Random.Range(0, 2)];
            int yPos = yRand[Random.Range(0, 2)];  

            Debug.Log("set ghost to pos : " + xPos + " and " + yPos);
            ghosts[i].GetComponent<LockUIItem>().SetPosition(new Vector2(xPos, yPos));
        }
    }

    void Update()
    {
        if (!ghostScript.end)
        {
            // Right
            if (Input.GetKey(KeyCode.RightArrow) && GetComponent<RectTransform>().localPosition.x > -xLimit)
            {
                transform.parent.Translate(Vector2.right * movingMultiplier);
            }

            // Left
            if (Input.GetKey(KeyCode.LeftArrow) && GetComponent<RectTransform>().localPosition.x < xLimit)
            {
                transform.parent.Translate(Vector2.right * -movingMultiplier);
            }

            // Down
            if (Input.GetKey(KeyCode.DownArrow) && GetComponent<RectTransform>().localPosition.y > -yLimit)
            {
                transform.parent.Translate(Vector2.up * -movingMultiplier);
            }

            // Up
            if (Input.GetKey(KeyCode.UpArrow) && GetComponent<RectTransform>().localPosition.y < yLimit)
            {
                transform.parent.Translate(Vector2.up * movingMultiplier);
            }
        }
    }
}
