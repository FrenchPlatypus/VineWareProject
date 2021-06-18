using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public int direction;
    public int multiplicator;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * direction * multiplicator);

        if (transform.localPosition.x <= -623)
        {
            direction = 1;
        }
        else if (transform.localPosition.x >= 623)
        {
            direction = -1;
        }
    }
}
