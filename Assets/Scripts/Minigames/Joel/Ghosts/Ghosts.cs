using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{

    [SerializeField] private StoryModeManager manager;
    [SerializeField] private float timer;
    [SerializeField] private int difficulty;
    [SerializeField] public int killCount;

    [SerializeField] public bool end;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(killCount >= difficulty + 1 && end == false)
        {
            end = true;
            manager.minigameWon = true;
        }
    }
}
