using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertBus : MonoBehaviour
{
    public StoryModeManager manager;
    public float timer;
    public int difficulty;

    public GameObject mario;
    public GameObject simpleFlips;
    public GameObject buttonImage;

    public Animator bus;
    public Animator busOver;
    public Animator road;

    public bool fail;
    public bool canClick;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        StartCoroutine(SetWinState());

        mario.SetActive(false);
        simpleFlips.SetActive(false);

        if (difficulty > 0)
        {
            mario.SetActive(true);
            //buttonImage.SetActive(true);

            if(difficulty == 2)
            {
                simpleFlips.SetActive(true);
            }
        }
    }

    public IEnumerator SetWinState()
    {
        yield return new WaitForSeconds(0.1f);
        manager.minigameWon = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(canClick == true)
            {

            }
            else
            {
              //  Fail();
            }
        }
    }

    public void Fail()
    {
        bus.enabled = false;
        road.enabled = false;
        busOver.enabled = false;

        fail = true;
        manager.minigameWon = false;
    }
}
