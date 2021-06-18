using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateObjectOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] objectsToActivate;

    public int[] changeIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }

        if (changeIndex != null)
        {
            gameObject.transform.SetSiblingIndex(changeIndex[0]);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(false);
        }

        if (changeIndex.Length == 2)
        {
            gameObject.transform.SetSiblingIndex(changeIndex[1]);
        }
    }
}
