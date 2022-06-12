using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSoda : MonoBehaviour
{
    [SerializeField] private GameInfoManager manager;
    [SerializeField] private float timer;

    [SerializeField] private int difficulty;

    [SerializeField] private bool awake;
    [SerializeField] private bool drawing;

    [SerializeField] private bool win;
    [SerializeField] private bool fail;

    [SerializeField] private int gameState;

    [SerializeField] private Animator minigameAnim;
    [SerializeField] private GameObject[] faces;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        manager.minigameWon = true;

        minigameAnim.Play(difficulty.ToString());
        minigameAnim.enabled = false;

        InvokeRepeating("SetWakeState", 1.5f - (difficulty/2), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(fail == true && drawing == true)
        {
            drawing = false;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                minigameAnim.enabled = true;
                drawing = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                minigameAnim.enabled = false;
                drawing = false;
            }
        }
    }

    private void SetWakeState()
    {
        if (!fail)
        {
            int state = Random.Range(0, 3);

            switch (state)
            {
                // State up
                case 0:
                    if (drawing)
                    {
                        faces[gameState].SetActive(false);
                        gameState++;

                        faces[gameState].SetActive(true);
                    }
                    break;
                // state still
                case 1:
                    break;
                // state down
                case 2:
                    if (!drawing && gameState > 0)
                    {
                        faces[gameState].SetActive(false);
                        gameState--;

                        faces[gameState].SetActive(true);
                    }
                    break;
            }

            if (gameState == 2)
            {
                fail = true;
                manager.minigameWon = false;
            }
        }
    }
}
