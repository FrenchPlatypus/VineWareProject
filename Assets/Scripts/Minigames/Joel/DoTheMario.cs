using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoTheMario : MonoBehaviour
{
    public GameInfoManager manager;

    public float timer;

    public bool wrongInput;
    public bool end;

    public ArrowMove[] arrowMoves;
    public ArrowMove[] arrowButtons;
    public int actualStep;

    public Sprite[] marioSprites;

    public Image marioModel;
    public Transform buttonParent;

    public AudioClip[] marioSounds;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        marioModel.GetComponent<Animator>().enabled = false;
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        for(int i = 0; i < arrowButtons.Length; i++)
        {
            int random = Random.Range(0, 4);

            arrowButtons[i] = arrowMoves[random];

            GameObject instance = Instantiate(arrowButtons[i].button, buttonParent);
        }
    }

    private void Update()
    {
        if(actualStep < arrowButtons.Length && wrongInput == false)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(arrowButtons[actualStep].input))
                {
                    actualStep++;
                    Destroy(buttonParent.GetChild(0).gameObject);
                }
                else if (!Input.GetKeyDown(arrowButtons[actualStep].input))    
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        wrongInput = true;
                    }
                }
            }
        }
        else if ( end == false)
        {
            end = true;
            MinigameFinished();
        }

        if (end == false)
        {
            // Get key Down
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                marioModel.sprite = marioSprites[2];
                GetComponent<AudioSource>().clip = marioSounds[0];
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                marioModel.sprite = marioSprites[3];
                GetComponent<AudioSource>().clip = marioSounds[1];
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                marioModel.sprite = marioSprites[4];
                GetComponent<AudioSource>().clip = marioSounds[2];
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                marioModel.sprite = marioSprites[1];
                GetComponent<AudioSource>().clip = marioSounds[3];
                GetComponent<AudioSource>().Play();
            }

            // Get key Up
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                marioModel.sprite = marioSprites[0];
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                marioModel.sprite = marioSprites[0];
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                marioModel.sprite = marioSprites[0];
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                marioModel.sprite = marioSprites[0];
            }
        }
    }

    public void MinigameFinished()
    {
        if (wrongInput == false)
        {
            manager.minigameWon = true;
            marioModel.GetComponent<Animator>().enabled = true;
           transform.GetChild(0).GetComponent<AudioSource>().clip = marioSounds[4];
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        else
        {
            transform.GetChild(0).GetComponent<AudioSource>().clip = marioSounds[5];
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }

    [System.Serializable]
    public class ArrowMove
    {
       public KeyCode input;
       public GameObject button;
    }
}
