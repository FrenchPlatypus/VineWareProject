using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugSnax : MonoBehaviour
{
    [SerializeField] private StoryModeManager manager;

    [SerializeField] private int difficulty;
    [SerializeField] private float timer;

    [SerializeField] private Transform billy;
    [SerializeField] private Vector2[] billyPos;
    [SerializeField] private bool billyDead;

    [SerializeField] private Transform barry;
    [SerializeField] private bool moveLeft;
    [SerializeField] private bool barryDead;

    [SerializeField] private Animator shotgun;

    [SerializeField] private Transform bgMove;

    private float limits;

    private float recharge;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InitializeLevel();
    }

    private void InitializeLevel()
    {
        int barryPosX = Random.Range(-904, 905);
        barry.localPosition = new Vector2(barryPosX, 0);

        if(difficulty > 0)
        {
            StartCoroutine(BarryMove());

            if (difficulty == 2)
                StartCoroutine(BillyMove());
        }
    }

    private IEnumerator BillyMove()
    {
        billy.gameObject.SetActive(true);

        while (!billyDead)
        {
            yield return new WaitForSeconds(2);
            billy.localPosition = billyPos[Random.Range(0, billyPos.Length)];
            yield return null;
        }

    }

    private IEnumerator BarryMove()
    {
        while (!barryDead)
        {
            if (moveLeft)
            {
                barry.Translate(Vector2.left * 2);

                if (barry.localPosition.x <= -904)
                    moveLeft = false;
            }
            else
            {
                barry.Translate(Vector2.right * 2);

                if (barry.localPosition.x >= 904)
                    moveLeft = true;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") <= -0.2f && bgMove.localPosition.x <= 920)
        {
            bgMove.Translate(Vector2.right * 10);
        }
        if (Input.GetAxis("Mouse X") >= 0.2f && bgMove.localPosition.x >= -920)
        {
            bgMove.Translate(Vector2.left * 10);
        }

        recharge--;

        if (Input.GetMouseButtonDown(0) && recharge <= 0)
        {
            recharge = 1;
            shotgun.Play("Recharge");

            float barryPos = barry.localPosition.x + bgMove.localPosition.x;
            float billyPos = billy.localPosition.x + bgMove.localPosition.x;

            Debug.Log("Barry pos : " + barryPos);
            Debug.Log("Billy pos : " + billyPos);

            if (barryPos <= 157 && barryPos >= -157)
            {
                barryDead = true;
                barry.GetComponent<Animator>().Play("BarryFade");
                barry.GetChild(0).gameObject.SetActive(true);
                CheckWin();
            }

            if(difficulty == 2 && billyPos <= 100 && billyPos >= -100)
            {
                billyDead = true;
                billy.GetComponent<Animator>().Play("BillyFade");
                billy.GetChild(0).gameObject.SetActive(true);
                CheckWin();
            }
        }
    }

    private void CheckWin()
    {
        switch (difficulty)
        {
            case 0:
            case 1:
                if (barryDead)
                    MinigameWon();
                break;
            case 2:
                if (barryDead && billyDead)
                    MinigameWon();
                break;
        }
    }

    public void MinigameWon()
    {
        manager.minigameWon = true;
        if (manager.minigameTimer > 1)
        {
            manager.minigameTimer = 1;
        }
    }

    public void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
