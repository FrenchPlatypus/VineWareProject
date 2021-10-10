using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShoeAnim : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Animator anim;
    public bool inHitbox;

    void Start()
    {
        anim.speed = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.speed = 2;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.speed = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.speed = 0;
    }
}
