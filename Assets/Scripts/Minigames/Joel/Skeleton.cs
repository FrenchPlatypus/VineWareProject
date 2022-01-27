using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skeleton : MonoBehaviour
{
    public StoryModeManager manager;

    public float timer;
    public float progress;

    public Animator skeletonPivot;
    public Animator guitarPivot;

    public Image score;

    public int multiplicator;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        skeletonPivot.speed = 0;
        guitarPivot.speed = 0;

        InvokeRepeating("UpdateProgress", 0, 0.2f);
    }

    public void UpdateProgress()
    {
       // guitarPivot.enabled = false;
      //  skeletonPivot.enabled = false;
    }

    void Update()
    {
        if (progress > 0 && progress < 100)
        {
            progress--;
            score.fillAmount = progress/100;
            skeletonPivot.speed = (progress / 20);
            guitarPivot.speed = (progress / 20);

            Debug.Log(skeletonPivot.speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && progress < 100)
        {
            skeletonPivot.enabled = true;
            progress += multiplicator;
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.DownArrow))
            && progress < 100)
        {
            guitarPivot.enabled = true;
            progress += multiplicator;
        }

        if (progress >= 100 && manager.minigameTimer > 0)
        {
            MinigameWon();
            CancelInvoke();
        }
    }

    public void MinigameWon()
    {
        score.fillAmount = 100;
        manager.minigameWon = true;
        skeletonPivot.Play("Win");

        if(manager.minigameTimer > 1)
        {
            manager.minigameTimer = 1;
        }
    }  
}
