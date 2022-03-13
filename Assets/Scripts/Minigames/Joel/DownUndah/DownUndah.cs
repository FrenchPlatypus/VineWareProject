using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownUndah : MonoBehaviour
{
    public GameInfoManager manager;
    public int difficulty;

    public float timer;
    public float animationTimer;

    public Vector2[] spawnPositions;

    public Transform danCharacter;
    public GameObject pelicanPrefab;
    public Transform pelicanParent;

    public bool fail;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        StartCoroutine(SetWinState());

        InvokeRepeating("Spawn", 1.0f, 3f - difficulty/2);
    }

    public IEnumerator SetWinState()
    {
        yield return new WaitForSeconds(0.1f);
        manager.minigameWon = true;
    }

    void Update()
    {
        if (fail == false)
        {
            animationTimer -= Time.deltaTime;
            // Launch animation
            if (Input.GetKeyDown(KeyCode.Space) && animationTimer <= 0)
            {
                danCharacter.GetChild(1).GetComponent<Animator>().Play("DanTurn");
                animationTimer = 1.5f;
            }

            danCharacter.Translate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) *Time.deltaTime * 500);
        }
    }

    public void Spawn()
    {
        StartCoroutine(SpawnPelican());
    }

    public IEnumerator SpawnPelican()
    {
        Vector3 target = danCharacter.localPosition;
        Vector3 spawnPosition = spawnPositions[Random.Range(0, 4)];

        Vector3 moveVector =(target - spawnPosition).normalized;

        GameObject pelicanInstance = Instantiate(pelicanPrefab, pelicanParent);
        pelicanInstance.transform.localPosition = spawnPosition;

        pelicanInstance.SetActive(true);

        float timer = 10;
        while (timer > 0 || fail == false)
        {
            timer -= Time.deltaTime;
            pelicanInstance.transform.Translate(moveVector * Time.deltaTime * 400);
            yield return null;
        }

        Destroy(pelicanInstance);
        yield return null;
    }

    public void Fail()
    {
        danCharacter.GetChild(0).gameObject.SetActive(true);
        fail = true;
        manager.minigameWon = false;
    }
}
