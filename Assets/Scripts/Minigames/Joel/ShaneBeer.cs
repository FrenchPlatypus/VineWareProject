using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShaneBeer : MonoBehaviour
{
    public GameInfoManager manager;
    public int difficulty;

    public float timer;

    public GameObject beerAnim;
    public Transform hands;
    public Transform endParent;

    public int anim;
    public bool clicked;

    public bool win;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        BeerInitialize();
    }

    public void BeerInitialize()
    {
        anim = Random.Range(0, difficulty + 1);
        float timeBeforeBeer = Random.Range(1, 3);

        StartCoroutine(LaunchBeer("BeerSlide" + anim, timeBeforeBeer));
    }

    public IEnumerator LaunchBeer(string animName, float time)
    {
        Debug.Log("play : " + animName);
        yield return new WaitForSeconds(time);

        beerAnim.SetActive(true);
        hands.GetComponent<Animator>().Play(animName);

    }

    public IEnumerator HandAnim(int child)
    {
        hands.GetChild(child).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        if(win == false)
        {
            hands.GetChild(child).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && clicked == false)
        {
            clicked = true;
            if (anim == 0 || (anim == 2 && beerAnim.transform.GetChild(2).gameObject.activeSelf == false))
            {
                StartCoroutine(HandAnim(0));
            }
            else
            {
                StartCoroutine(HandAnim(2));
            }

            if(beerAnim.transform.GetChild(0).gameObject.activeSelf == true)
            {
                endParent.GetChild(0).gameObject.SetActive(true);
                win = true;
                manager.minigameWon = true;
            }
            else
            {
                endParent.GetChild(1).gameObject.SetActive(true);
            }
        }

        if(beerAnim.transform.GetChild(1).gameObject.activeSelf == true && win == true)
        {
            hands.GetComponent<Animator>().enabled = false;
        }
    }
}
