using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepItOut : MonoBehaviour
{
    public StoryModeManager manager;
    public int difficulty;
    public float timer;

    public Transform ennemyParent;
    public Transform joel;
    public Transform door;

    public float multiplier;

    public bool fail;
    public bool started;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        StartCoroutine(SetWinState());

    }

    public IEnumerator SetWinState()
    {
        yield return new WaitForSeconds(0.1f);
        manager.minigameWon = true;

        yield return new WaitForSeconds(1f);
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (started == true)
        {
            if (fail == false)
            {
                multiplier -= Time.deltaTime * (difficulty + 1) * 22;

                if (Input.GetKeyDown(KeyCode.Space) && multiplier < 40)
                {
                    multiplier += 7;
                }

                if (multiplier < -40)
                {
                    Fail();
                }
            }
            else
            {
                multiplier -= Time.deltaTime * (difficulty + 1) * 1000;
            }

            joel.GetComponent<RectTransform>().localPosition = new Vector3(multiplier * -1, 0, 0);
            ennemyParent.GetComponent<RectTransform>().localPosition = new Vector3(multiplier * -1, 0, 0);
            door.GetComponent<RectTransform>().localPosition = new Vector3(multiplier * -1, 0, 0);
        }
    }

    public void Fail()
    {
        fail = true;
        manager.minigameWon = false;
    }
}
