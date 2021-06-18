using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BBQ_V2 : MonoBehaviour, IPointerClickHandler
{
    public StoryModeManager manager;

    public int difficulty;

    public float timer;

    public Image switchButton;
    public Sprite switchOn;

    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Shoe On");
        WinMinigame();
    }

    public void WinMinigame()
    {
        manager.minigameWon = true;
        switchButton.sprite = switchOn;
    }
}
