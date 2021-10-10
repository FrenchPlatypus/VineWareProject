using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DestroyObject", timer, timer);
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
