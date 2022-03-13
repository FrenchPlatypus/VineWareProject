using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racoon : MonoBehaviour
{
    public GameInfoManager manager;

    public int difficulty;

    public float timer;
    public float jumpTimer;

    public Transform racoonCharacter;
    public Transform obstacles;

    public bool crouched;

    public bool fail;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        StartCoroutine(SetWinState());

        InvokeRepeating("Spawn", 1.0f, 3f - difficulty / 2);

        obstacles.GetChild(difficulty).gameObject.SetActive(true);

        int rand = Random.Range(0, 2);

        obstacles.GetChild(difficulty).GetChild(rand).gameObject.SetActive(true);

    }

    public IEnumerator SetWinState()
    {
        yield return new WaitForSeconds(0.1f);
        manager.minigameWon = true;
    }

    private void Update()
    {
        if(fail == false)
        {
            jumpTimer -= Time.deltaTime;

            obstacles.Translate(Vector2.left * 2);

            if (Input.GetKeyDown(KeyCode.Space) && jumpTimer < 0)
            {
                jumpTimer = 1.3f;
                racoonCharacter.GetComponent<Animator>().Play("RacoonJump");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                crouched = true;
                racoonCharacter.GetComponent<Animator>().Play("RacoonCrouch");
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {   
                crouched = true;
                racoonCharacter.GetComponent<Animator>().Play("RacoonUp");
            }
        }
    }


    public void FailGame()
    {
        fail = true;
        manager.minigameWon = false;
        racoonCharacter.GetComponent<Animator>().enabled = false;
    }
}
