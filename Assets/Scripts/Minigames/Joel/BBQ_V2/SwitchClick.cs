using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchClick : MonoBehaviour, IPointerClickHandler
{
    public BBQ_V2 gameScript;
    public Sprite switchOn;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Shoe On");
        gameScript.SwitchClick();

        GetComponent<Image>().enabled = false;
        transform.parent.GetComponent<Image>().sprite = switchOn;
    }
}
