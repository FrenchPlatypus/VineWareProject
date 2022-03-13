using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monkey : MonoBehaviour
{
    public GameInfoManager manager;

    public float timer;
    public float progress;

    public float shitTimer;

    public Transform monkey;
    public Transform bg;

    public GameObject boganShit;

    public GameObject shit;

    public bool minkyDed;

    public int massageeMultiplicator;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InvokeRepeating("UpdateProgress", 1.0f, 0.08f);
    }

    void Update()
    {
        monkey.transform.Translate(Vector2.down * Time.deltaTime * 200);
        bg.transform.Translate(Vector2.left * Time.deltaTime * 300);

        if(shitTimer > 0)
        {
            shitTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && minkyDed == false)
        {
            monkey.transform.Translate(Vector2.up * 100);
            monkey.GetComponent<Animator>().Play("MonkeyJump");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            monkey.GetComponent<Animator>().Play("Monkey");
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (shitTimer <= 0 && minkyDed == false)
            {
                shitTimer = 2;
                GameObject shitInstance = Instantiate(shit, monkey);
                shitInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, -210);
                shitInstance.transform.SetParent(transform);
                StartCoroutine(ShitFall(shitInstance));
            }     
        }

        if (monkey.GetComponent<ColliderEnterObject>().enteredTrigger == true)
        {
            monkey.transform.GetChild(0).gameObject.SetActive(true);
            minkyDed = true;
        }

    }

    public IEnumerator ShitFall(GameObject shit)
    {
        while(shit.GetComponent<ColliderEnterObject>().enteredTrigger == false)
        {
            shit.transform.transform.Translate(Vector2.down * Time.deltaTime * 1000);
            yield return null;
        }

        if( minkyDed == false)
        {
            boganShit.SetActive(true);
            manager.minigameWon = true;
            Destroy(shit);
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
}
