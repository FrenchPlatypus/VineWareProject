using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public StoryModeManager manager;
    public float timer;
    public int difficulty;
    public int killCount;

    public Transform characters;

    public bool fire;
    public bool end;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (fire == false)
        {
            if (Input.GetKey(KeyCode.RightArrow) && characters.localPosition.x < 548)
            {
                characters.Translate(Vector2.right * 10);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && characters.localPosition.x > -492)
            {
                characters.Translate(Vector2.left * 10);
            }

            if(cooldown <= 0 && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(LaunchBeam());
            }
        }

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if(killCount >= difficulty + 1 && end == false)
        {
            end = true;
            manager.minigameWon = true;
        }
    }

    public IEnumerator LaunchBeam()
    {
        fire = true;
        characters.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
     
        characters.GetChild(0).gameObject.SetActive(false);
        cooldown = 0.7f;
        fire = false;
    }
}
