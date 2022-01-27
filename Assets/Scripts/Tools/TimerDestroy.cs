using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObject());
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(timer);

        Destroy(this.gameObject);
    }
}
