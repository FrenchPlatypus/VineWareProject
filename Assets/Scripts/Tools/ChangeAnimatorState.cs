using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimatorState : MonoBehaviour
{
    public string parameter;

    public void ChangeState(int state)
    {
        GetComponent<Animator>().SetInteger(parameter, state);
    }
}
