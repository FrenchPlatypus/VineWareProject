using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assemble : MonoBehaviour
{
    public StoryModeManager manager;
    public int difficulty;

    public float timer;
    public int speed;

    public Transform modelsParent;
    public Transform bottomControl;
    public GameObject win;

    public bool fail;
    public int step;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InitializeControl();
    }

    public void InitializeControl()
    {
        int modelIndex = Random.Range(0, difficulty + 1) +1;

        modelsParent.GetChild(modelIndex).gameObject.SetActive(true);
        bottomControl = modelsParent.GetChild(modelIndex).GetChild(0).GetChild(0);

        StartCoroutine(BodyLaunch());
    }

    public IEnumerator BodyLaunch()
    {
        yield return new WaitForSeconds(2);
        SetBodyPos(1);
        yield return new WaitForSeconds(1.7f);
        SetBodyPos(2);
        if(bottomControl.parent.parent.GetSiblingIndex() == 3)
        {
            yield return new WaitForSeconds(1.7f);
            SetBodyPos(3);
        }
    }

    public void SetBodyPos(int index)
    {
        int posX = Random.Range(-450, 451);
        bottomControl.parent.parent.GetChild(index).localPosition = new Vector2(0, 0);

        if(difficulty > 0)
        {
            bottomControl.parent.parent.GetChild(index).GetComponent<Animator>().enabled = true;
        }

        bottomControl.parent.parent.GetChild(index).GetChild(0).localPosition = new Vector2(posX, 721);
        bottomControl.parent.parent.GetChild(index).GetChild(0).gameObject.SetActive(true);

        StartCoroutine(MovingBody(bottomControl.parent.parent.GetChild(index).GetChild(0).GetComponent<AssembleCollider>(), index));
    }


    public IEnumerator MovingBody(AssembleCollider body, int index)
    {
        while (body.GetComponent<RectTransform>().localPosition.y > body.posY && fail == false)
        {
            body.GetComponent<RectTransform>().Translate(Vector2.down * speed);
            yield return null;
        }

        bottomControl.parent.parent.GetChild(index).GetComponent<Animator>().enabled = false;
        Vector2 pos = body.GetComponent<RectTransform>().localPosition;

        float limitR = bottomControl.parent.parent.GetChild(index - 1).GetChild(0).GetComponent<RectTransform>().localPosition.x + body.posXR;
        float limitL = bottomControl.parent.parent.GetChild(index - 1).GetChild(0).GetComponent<RectTransform>().localPosition.x - body.posXL;

        Vector2 debugPos = new Vector2(limitL, limitR);

        Debug.Log("BodyPos x : " + pos.x);
        Debug.Log("DebugPos : " + debugPos);

        if( fail == false)
        {
            if (pos.x >= limitL && pos.x <= limitR)
            {
                step++;
            }
            else
            {
                FailMinigame();
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (bottomControl != null && fail == false)
        {
            if ((bottomControl.parent.parent.GetSiblingIndex() < 3 && step < 2) || (bottomControl.parent.parent.GetSiblingIndex() == 3 && step < 3))
            {
                if (Input.GetKey(KeyCode.RightArrow) && bottomControl.localPosition.x < 764f)
                {
                    bottomControl.Translate(Vector2.right * 10);
                    if (step > 0)
                    {
                        bottomControl.parent.parent.GetChild(1).GetChild(0).Translate(Vector2.right * 10);
                        if (step > 1)
                        {
                            bottomControl.parent.parent.GetChild(2).GetChild(0).Translate(Vector2.right * 10);
                        }
                    }
                }

                if (Input.GetKey(KeyCode.LeftArrow) && bottomControl.localPosition.x > -641f)
                {
                    bottomControl.Translate(Vector2.left * 10);
                    if (step > 0)
                    {
                        bottomControl.parent.parent.GetChild(1).GetChild(0).Translate(Vector2.left * 10);
                        if (step > 1)
                        {
                            bottomControl.parent.parent.GetChild(2).GetChild(0).Translate(Vector2.left * 10);
                        }
                    }
                }
            }
        }

        if((bottomControl.parent.parent.GetSiblingIndex() < 3 && step == 2) && fail == false)
        {
            manager.minigameWon = true;
            win.SetActive(true);
        }
        else if((bottomControl.parent.parent.GetSiblingIndex() == 3 && step == 3) && fail == false)
        {
            manager.minigameWon = true;
            win.SetActive(true);
        }
    }

    public void FailMinigame()
    {
        fail = true;

        if (manager.minigameTimer > 2)
        {
            manager.minigameTimer = 2;
        }
    }
}
