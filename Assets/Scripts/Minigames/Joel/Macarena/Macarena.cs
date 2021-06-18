using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Macarena : MonoBehaviour
{
    public StoryModeManager manager;
    public float timer;

    public Transform woman;
    public int difficulty;

    public GameObject[] sharkPrefabs;
    public Transform sharkParent;
    public GameObject explosion;

    public bool fail;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        StartCoroutine(SetWinState());

        StartCoroutine(SharkMove(-1, -269, 0));
        StartCoroutine(SharkMove(1, 0, 2));

        if(difficulty > 0)
        {
            StartCoroutine(SharkMove(1, 0, 3.5f));
            if (difficulty > 1)
            {
                StartCoroutine(SharkMove(-1, 0, 5));
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
        if (fail == false)
        {
            if (Input.GetKey(KeyCode.Space) && woman.localPosition.y < 400)
            {
                woman.Translate(Vector2.up * 7f);
                woman.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                woman.GetChild(0).GetComponent<Animator>().Play("Woman");
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                woman.GetChild(0).GetComponent<Animator>().Play("Woman_Fall");
            }
        }
    }

    public IEnumerator SharkMove(int side, float height, float waitTime)
    {
        GameObject instance;
        float randomheight = Random.Range(-295, 344);
        if ( height == 0)
        {
            height = randomheight;
        }
      

        yield return new WaitForSeconds(waitTime);

        if(side == 1)
        {
            instance = Instantiate(sharkPrefabs[0], sharkParent);
            instance.transform.localPosition = new Vector2(1300, height);
        }
        else
        {
            instance = Instantiate(sharkPrefabs[1], sharkParent);
            instance.transform.localPosition = new Vector2(-1300, height);
        }

        while (instance != null)
        {
            instance.transform.Translate(Vector2.left * 5f * side);
            yield return null;
        }
     
    }

    public void FailMinigame()
    {
        explosion.SetActive(true);
        fail = true;

        manager.minigameWon = false;
    }
}
