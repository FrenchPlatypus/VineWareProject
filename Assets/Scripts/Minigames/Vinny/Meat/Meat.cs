using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    public float timer;
    public GameInfoManager manager;

    public Transform binny;
    public Transform meatMonster;

    public float lilMeatTimer;
    public GameObject meatPrefab;

    public bool airBorne;

    public AudioClip[] meatSounds;

    public int difficulty;

    public bool win;
    public bool lose;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        if (difficulty > 0)
        {
            Debug.Log("1");
            InvokeRepeating("InstantiateMeatInvoke", lilMeatTimer, lilMeatTimer);

            if( difficulty == 2)
            {
                Debug.Log("2");
                InvokeRepeating("InstantiateMeatInvoke", lilMeatTimer + lilMeatTimer/5 , lilMeatTimer *3);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (win == false && lose == false)
        {
            if (binny.localPosition.x < 510)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    binny.Translate(Vector3.right * 10);
                }
            }

            if (binny.localPosition.x > -526)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    binny.Translate(Vector3.left * 10);
                }
            }

            if (Input.GetKeyDown("space") && airBorne == false)
            {
                binny.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1000);
                airBorne = true;
            }
        }
    }

    public void InstantiateMeatInvoke()
    {
        StartCoroutine(InstantiateMeat());
    }

    public IEnumerator InstantiateMeat()
    {
        Debug.Log("blurg");
        meatMonster.GetComponent<AudioSource>().clip = meatSounds[1];
        meatMonster.GetComponent<AudioSource>().Play();

        meatMonster.GetChild(0).gameObject.SetActive(true);
        GameObject meatInstance = Instantiate(meatPrefab, meatMonster);
        meatInstance.transform.localPosition = new Vector2(284, -152);
        StartCoroutine(MeatProjectile(meatInstance));

        yield return new WaitForSeconds(0.5f);
        meatMonster.GetChild(0).gameObject.SetActive(false);
    }

    public IEnumerator MeatProjectile(GameObject projectile)
    {
        float timer = 0;
        while(timer < 5)
        {
            timer += Time.deltaTime;
            projectile.transform.Translate(Vector3.left * 10);
            yield return null;
        }
        Destroy(projectile);
    }

    public void BinnyDeath()
    {
        CancelInvoke();
        lose = true;
    }

    public void BinnyWin()
    {
        CancelInvoke();

        meatMonster.GetComponent<Animator>().Play("MeatDeath");
        meatMonster.GetComponent<AudioSource>().clip = meatSounds[0];
        meatMonster.GetComponent<AudioSource>().Play();

        win = true;
        manager.minigameWon = true;
    }
}
