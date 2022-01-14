using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoFish : MonoBehaviour
{
    [SerializeField] private StoryModeManager manager;
    [SerializeField] private float timer;

    [SerializeField] private Transform fishParent;
    [SerializeField] private Animator marioParent;

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
        if (Input.GetKeyDown(KeyCode.Space) && fishParent.GetComponent<Button>().enabled)
            MinigameWon();
    }

    public void MinigameWon()
    {
        fishParent.gameObject.SetActive(false);
        marioParent.enabled = true;
        manager.minigameWon = true;

        if (manager.minigameTimer > 2)
        {
            manager.minigameTimer = 2;
        }
    }
}
