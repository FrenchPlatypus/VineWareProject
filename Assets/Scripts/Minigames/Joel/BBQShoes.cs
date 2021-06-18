using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBQShoes : MonoBehaviour
{
    public StoryModeManager manager;

    public float timer;
    public float progress;

    public Transform[] shoesSprites;

    public bool pressedButton;

    public int moveIndex;
    public int moving;

    public float topLimit;
    public float botLimit;

    public float stopLose;
    public float stopWin;

    public bool win;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InvokeRepeating("UpdateProgress", 1.0f, 0.08f);
    }

    void Update()
    {
        if(pressedButton == false)
        {
            if(moveIndex == 1)
            {
                moving -= 1;
                shoesSprites[0].Translate(Vector2.down * 8);
                shoesSprites[1].Translate(Vector2.down * 8);

                if (moving <= 0)
                {
                    moveIndex = 0;
                }
            }
            else
            {
                moving += 1;
                shoesSprites[0].Translate(Vector2.up * 8);
                shoesSprites[1].Translate(Vector2.up * 8);

                if (moving >= 65)
                {
                    moveIndex = 1;
                }
            }           
        }
        else
        {
            if((win == true && shoesSprites[0].localPosition.x < stopWin) || ( win == false && shoesSprites[0].localPosition.x < stopLose))
            {
                shoesSprites[0].Translate(Vector2.right * 12);
                shoesSprites[1].Translate(Vector2.right * 12);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(pressedButton == false)
            {
                pressedButton = true;
                checkHeight();
            }
        }
    }

    public void checkHeight()
    {
        Debug.Log("height : " + shoesSprites[0].localPosition.y);
       
        if (shoesSprites[0].localPosition.y >= botLimit && shoesSprites[0].localPosition.y < topLimit)
        {
            win = true;
            Debug.Log("BBQ Win");
        }
        else
        {
            Debug.Log("BBQ Fail");
        }

        manager.minigameWon = true;
    }
}
