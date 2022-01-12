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
        collision.GetComponentInParent<Animator>().StopPlayback();
        ghostScript.killCount++;
    }

    private void Start()
    {
        for(int i = 0; i < ghosts.Length; i++)
        {
            int xRand = Random.Range(-xLimit, xLimit);
            int yRand = Random.Range(-yLimit, yLimit);

            Debug.Log("set ghost to pos : " + xRand + " and " + yRand);
            ghosts[i].GetComponent<LockUIItem>().SetPosition(new Vector2(xRand, yRand));
        }
    }

    void Update()
    {
        if (!ghostScript.end)
        {
            // Right
            if (Input.GetKey(KeyCode.RightArrow) && GetComponent<RectTransform>().localPosition.x > -xLimit)
            {
                transform.Translate(Vector2.right * movingMultiplier);
            }

            // Left
            if (Input.GetKey(KeyCode.LeftArrow) && GetComponent<RectTransform>().localPosition.x < xLimit)
            {
                transform.Translate(Vector2.right * -movingMultiplier);
            }

            // Down
            if (Input.GetKey(KeyCode.DownArrow) && GetComponent<RectTransform>().localPosition.y > -yLimit)
            {
                transform.Translate(Vector2.up * -movingMultiplier);
            }

            // Up
            if (Input.GetKey(KeyCode.UpArrow) && GetComponent<RectTransform>().localPosition.y < yLimit)
            {
                transform.Translate(Vector2.up * movingMultiplier);
            }
        }
    }
}
