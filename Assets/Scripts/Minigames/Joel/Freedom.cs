using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Freedom : MonoBehaviour
{
    public GameInfoManager manager;

    public bool win;

    public ArrowInput[] arrowInputs;
    public ArrowInput[] arrowButtons;
    public int actualStep;

    public GameObject pointsParent;

    public bool waitForInput;
    public bool wrongInput;
    public bool goodInput;

    public Animator bgAnimator;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = 1;
        manager.maxTimer = 1;

        for (int i = 0; i < arrowInputs.Length; i++)
        {
            int random = Random.Range(0, 4);

            arrowInputs[i] = arrowButtons[random];
        }

        StartCoroutine(Initialize());
    }

    public IEnumerator Initialize()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadArgument());
    }

    public IEnumerator LoadArgument()
    {
        goodInput = false;
        wrongInput = false;

        pointsParent.SetActive(true);
        pointsParent.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = arrowInputs[actualStep].buttonSprite;
        pointsParent.GetComponent<Animator>().Play("Points");

        yield return new WaitForSeconds(2);

        waitForInput = true;

        yield return new WaitForSeconds(1f);

        pointsParent.SetActive(false);
        waitForInput = false;

        if(goodInput == true)
        {
            actualStep++;

            if(actualStep == arrowInputs.Length)
            {
                bgAnimator.Play("WinGame");
                yield return new WaitForSeconds(3);
                win = true;
                MinigameFinished();
            }
            else
            {
                bgAnimator.Play("WinArgument", -1, 0f);
                yield return new WaitForSeconds(3);
                StartCoroutine(LoadArgument());
            }
        }
        else
        {
            bgAnimator.Play("LoseArgument");
            yield return new WaitForSeconds(3);
            MinigameFinished();
        }
    }

    private void Update()
    {
        if (waitForInput == true && goodInput == false && wrongInput == false)
        {
            if (Input.anyKeyDown)
            {              
                if (Input.GetKeyDown(arrowInputs[actualStep].input))
                {
                    goodInput = true;
                }
                else if (!Input.GetKeyDown(arrowInputs[actualStep].input))
                {
                    wrongInput = true;
                }
            }
        }      
    }

    public void MinigameFinished()
    {
        if(win == true)
        {
            manager.minigameWon = true;
        }  
        manager.minigameTimer = 0;
    }

    [System.Serializable]
    public class ArrowInput
    {
        public KeyCode input;
        public Sprite buttonSprite;
    }
}
