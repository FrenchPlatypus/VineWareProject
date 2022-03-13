using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigFeet : MonoBehaviour
{
    public GameInfoManager manager;

    public float timer;

    public Animator handAnimator;

    public int handsPos;
    public int progress;

    public GameObject[] progressImages;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InvokeRepeating("UpdateProgress", 1.0f, 0.08f);
    }

    private void Update()
    {
        // Get key Down
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (handsPos == 0)
            {
                handsPos = 1;
                progress += 2;

                handAnimator.SetInteger("handsState", 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (handsPos == 1)
            {
                handsPos = 0;               
                progress += 2;

                handAnimator.SetInteger("handsState", 0);
            }
        }
    }

    public void UpdateProgress()
    {
        if (progress >= 0 && progress < 100)
        {
            if (progress >= 33 && progress < 66)
            {
                progressImages[1].SetActive(false);
            }
            else if (progress >= 66)
            {
                progressImages[2].SetActive(false);
            }
        }

        if (progress >= 100 && manager.minigameTimer > 0)
        {
            MinigameWon();
            CancelInvoke();
        }
    }

    public void MinigameWon()
    {
        manager.minigameWon = true;
        progressImages[0].SetActive(true);

    }
}
