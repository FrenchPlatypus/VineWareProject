using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrMeow : MonoBehaviour
{
    public StoryModeManager manager;
    public float timer;

    public int difficulty;

    public int randomMeow;
    public bool canEat;

    public bool win;
    public bool fail;

    public Transform meowParent;
    public Transform carrotParent;

    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        randomMeow = Random.Range(1, 3);

        StartCoroutine(MeowEat(randomMeow));
    }

    public IEnumerator MeowEat(int time)
    {
        //Wait before enabling eat
        yield return new WaitForSeconds(randomMeow);
        Debug.Log("Meow1");
        canEat = true;
        meowParent.GetChild(0).gameObject.SetActive(false);
        meowParent.GetChild(1).gameObject.SetActive(true);


        // Wait before disabling eat
        yield return new WaitForSeconds(1.5f - (difficulty/2));
        Debug.Log("Meow2");
        canEat = false;

        if(win == false)
        {
            meowParent.GetChild(1).gameObject.SetActive(false);
            meowParent.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && win == false && fail == false)
        {
            if(canEat == true)
            {
                MinigameFinished();
            }
            else
            {
                Fail();
            }
        }
    }

    public void Fail()
    {
        carrotParent.GetChild(0).gameObject.SetActive(false);
        carrotParent.GetChild(1).gameObject.SetActive(true);

        fail = true;

        if (manager.minigameTimer > 1)
        {
            manager.minigameTimer = 1;
        }
    }

    public void MinigameFinished()
    {
        win = true;
        manager.minigameWon = true;

        meowParent.GetChild(0).gameObject.SetActive(false);
        meowParent.GetChild(1).gameObject.SetActive(false);
        meowParent.GetChild(2).gameObject.SetActive(true);

        carrotParent.GetChild(0).gameObject.SetActive(false);
        carrotParent.GetChild(1).gameObject.SetActive(true);

        if (manager.minigameTimer > 1)
        {
            manager.minigameTimer = 1;
        }
    }
}
