using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShaveJoel : MonoBehaviour
{
    public StoryModeManager manager;

    public float timer;
    public float progress;

    public Transform shaverPivot;
    public Transform[] hairs;

    public GameObject[] smiles;

    public int hairMultiplicator;

    public Vector2 mousePosition;
    public Vector2 lastMousePos;
    public float distance;

    public bool shavingOn;
    public float shavingTimer;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InvokeRepeating("UpdateProgress", 1.0f, 0.08f);
        InvokeRepeating("CheckMouseMouvement", 1.0f, 0.01f);
    }

    void Update()
    {
        shavingTimer--;

        mousePosition = Input.mousePosition;
        shaverPivot.position = mousePosition;
    }

    public void CheckMouseMouvement()
    {
     

        Vector2 pos1 = new Vector2(0, mousePosition.y);
        Vector2 pos2 = new Vector2(0, lastMousePos.y);
            
        distance = Vector2.Distance(pos1, pos2);
        Debug.Log(distance);

        if (distance > 25)
        {
            shavingTimer = 20f;
        }
     
        if(shavingTimer > 0)
        {
            if (shavingOn == false)
            {
                shavingOn = true;
                GetComponent<AudioSource>().Play();
                shaverPivot.GetComponent<Animator>().Play("Shaving");
            }
        }
        else
        {
            if (shavingOn == true)
            {
                shavingOn = false;
                GetComponent<AudioSource>().Stop();
                shaverPivot.GetComponent<Animator>().Play("Idle");
            }
        }

        lastMousePos = mousePosition;
    }

    public void UpdateProgress()
    {
        

        if (progress >= 0 && progress < 100)
        {
            if (shavingOn == true)
            {
                progress += hairMultiplicator;
            }
            if (progress >= 16 && progress < 32)
            {
                StartCoroutine(HairFall(hairs[0]));
            }
            else if (progress >= 32 && progress < 48)
            {
                StartCoroutine(HairFall(hairs[1]));
            }
            else if (progress >= 48 && progress < 64)
            {
                StartCoroutine(HairFall(hairs[2]));
            }
            else if (progress >= 64 && progress < 80)
            {
                StartCoroutine(HairFall(hairs[3]));
            }
            else if (progress >= 80)
            {
                StartCoroutine(HairFall(hairs[4]));
            }
        }

        if(progress >= 100 && manager.minigameTimer > 0)
        {
            StartCoroutine(HairFall(hairs[5]));
            MinigameWon();
            CancelInvoke();
        }
    }

    public IEnumerator HairFall(Transform hair)
    {
        while(hair.GetComponent<RectTransform>().position.y > -1206f)
        {
            hair.Translate(Vector3.down * 10);
            yield return null;
        }
    }

    public void MinigameWon()
    {
        GetComponent<AudioSource>().Stop();
        shaverPivot.GetComponent<Animator>().Play("Idle");

        manager.minigameWon = true;
        smiles[1].SetActive(false);
        smiles[0].SetActive(true);
    }  
}
