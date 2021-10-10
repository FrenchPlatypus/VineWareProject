using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEm : MonoBehaviour
{
    public StoryModeManager manager;
    public int difficulty;
    public float timer;

    public Transform chatParent;
    public Transform earthParent;

    public GameObject eyes;

    public GameObject laserPrefab;
    public GameObject poofPrefab;

    public bool win;

    int earthHP;

    public float timerLaser;
    // Start is called before the first frame update
    void Start()
    {
        earthHP = difficulty + 1;
        changeEarthPos();
            
        if(difficulty > 0)
        {
            InvokeRepeating("changeEarthPos", (3 / difficulty) , (3 / difficulty) );
        }
    }

    public void changeEarthPos()
    {
        float randX = Random.Range(162, 830);
        float randY = Random.Range(-395, 132);

        earthParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(randX, randY);

        Instantiate(poofPrefab, earthParent);
    }

    // Update is called once per frame
    void Update()
    {
        chatParent.LookAt(Input.mousePosition);

        if(timerLaser <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject laserInstance;
                laserInstance = Instantiate(laserPrefab, chatParent.GetChild(0));
                laserInstance.transform.SetParent(this.transform);

                StartCoroutine(Pew(laserInstance.transform));

                timerLaser = 1;

            }
            eyes.SetActive(true);
        }
        else
        {
            timerLaser -= Time.deltaTime;
            eyes.SetActive(false);
        }
    }

    public IEnumerator Pew(Transform laser)
    {
        while (laser != null)
        {
            laser.Translate(Vector3.right * 40);
            yield return null;
        }
    }

    public void earthDamage()
    {
        earthHP--;

        if(earthHP <= 0)
        {
            manager.minigameWon = true;
        }
    }
}
