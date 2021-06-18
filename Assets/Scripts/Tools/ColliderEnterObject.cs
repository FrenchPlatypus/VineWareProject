using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnterObject : MonoBehaviour
{
    public bool enteredTrigger;
    public bool exitTrigger;
    public string tagToEnter;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("bleh");
        if (other.tag == tagToEnter)
        {
            enteredTrigger = true;
        }       
    }

    public void OnTriggerExit(Collider other)
    {
     
        if (other.tag == tagToEnter && exitTrigger == true)
        {
            enteredTrigger = false;
        }
    }
}
