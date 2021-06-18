using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleCollider : MonoBehaviour
{
    public Assemble assembleScript;
    public float posX;
    public bool collisionOn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionOn == false && assembleScript.fail == false)
        {
            Debug.Log("collide at y = " + transform.localPosition.y);
            collisionOn = true;
            if (transform.localPosition.y < posX)
            {
                assembleScript.fail = true;
                Debug.Log("fail");
            }
            else
            {
                transform.parent.GetComponent<Animator>().enabled = false;
                assembleScript.step++;
                Debug.Log("good");
            }
        }
    }
}
