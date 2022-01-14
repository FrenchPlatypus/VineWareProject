using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanCollide : MonoBehaviour
{
    public DownUndah minigameScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Cranky !");
        if (minigameScript.fail == false)
        {
            minigameScript.Fail();
            //transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
