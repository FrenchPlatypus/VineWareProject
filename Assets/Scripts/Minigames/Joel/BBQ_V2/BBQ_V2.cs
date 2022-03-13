using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BBQ_V2 : MonoBehaviour
{
    public GameInfoManager manager;

    public int difficulty;

    public float timer;

    public Image switchButton;
    public Sprite switchOn;

    public int clicks;
    public int maxClicks;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;
    }

    public void SwitchClick()
    {
        clicks++;
        if(clicks == maxClicks)
        {
            WinMinigame();
        }
    }

    public void WinMinigame()
    {
        Debug.Log("win");
        manager.minigameWon = true;
        switchButton.sprite = switchOn;
    }
}
