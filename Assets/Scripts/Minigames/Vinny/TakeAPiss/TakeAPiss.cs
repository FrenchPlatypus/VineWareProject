using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAPiss : MonoBehaviour
{
    public StoryModeManager manager;
    public int difficulty;
    public float timer;

    public Transform pissParent;
    public Transform peepee;
    public Transform backGround;

    public GameObject[] ennemies;
    public GameObject princess;

    public GameObject pissPrefab;

    public int ennemyKills;

    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InvokeRepeating("SpawnPiss", 0.05f, 0.05f);

        ennemies[0].SetActive(true);
        StartCoroutine(EnnemyMove(ennemies[0].transform, 0.5f));
        if ( difficulty > 0)
        {
            ennemies[1].SetActive(true);
            StartCoroutine(EnnemyMove(ennemies[1].transform, 1));

            if(difficulty == 2)
            {
                ennemies[2].SetActive(true);
                StartCoroutine(BossMove(ennemies[2].transform));
            }
        }
    }

    public void SpawnPiss()
    {
        GameObject instance = Instantiate(pissPrefab, pissParent);

        instance.transform.SetParent(backGround, true);
        StartCoroutine(PissMove(instance));
    }

    public IEnumerator EnnemyMove(Transform ennemy, float speed)
    {
        while(ennemy != null)
        {
            ennemy.transform.Translate(Vector2.left * speed);

            yield return null;
        }
    }

    public IEnumerator BossMove(Transform ennemy)
    {
        int turn = 0;

        while (ennemy != null)
        {
            turn++;

            if (turn < 1000)
            {
                ennemy.transform.Translate(Vector2.left/3);
            }
            else if (turn < 1500)
            {
                ennemy.transform.Translate(Vector2.right/3);
            }
            else turn = 0;

            yield return null;
        }
    }

    public IEnumerator PissMove(GameObject pissInstance)
    {
        while (pissInstance != null)
        {
            pissInstance.transform.Translate(Vector2.right * 7);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        peepee.LookAt(Input.mousePosition);

        if(ennemyKills == difficulty + 1)
        {
            ennemyKills++;
            StartCoroutine(WinMinigame());
        }
    }

    public IEnumerator WinMinigame()
    {
        princess.SetActive(true);

        if (manager.minigameTimer > 2)
            manager.minigameTimer = 2;

        while(princess!= null)
        {
            princess.transform.Translate(Vector2.down/2);
            yield return null;
        }
    }
}
