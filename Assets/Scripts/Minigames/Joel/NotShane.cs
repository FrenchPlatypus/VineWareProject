using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotShane : MonoBehaviour
{
    public StoryModeManager manager;

    public float timer;

    public GameObject badButton;
    public GameObject goodButton;

    public Transform buttonsParent;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        int rightButton = Random.Range(0, 2);

        Transform goodB;
        Transform badB;

        if (rightButton == 0)
        {
            badB = Instantiate(badButton, buttonsParent.GetChild(0)).transform;
            goodB = Instantiate(goodButton, buttonsParent.GetChild(1)).transform;
        }
        else
        {
            badB = Instantiate(badButton, buttonsParent.GetChild(1)).transform;
            goodB = Instantiate(goodButton, buttonsParent.GetChild(0)).transform;
        }

        badB.transform.localPosition = new Vector3(0, 0, 0);
        goodB.transform.localPosition = new Vector3(0, 0, 0);

        badB.GetComponent<Button>().onClick.AddListener(() => PressButton(1));
        goodB.GetComponent<Button>().onClick.AddListener(() => PressButton(0));
    }
    
    public void PressButton(int value)
    {
        Debug.Log("SHANE");
        buttonsParent.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
        buttonsParent.GetChild(1).GetChild(0).GetComponent<Button>().interactable = false;

        if (value == 0)
        {
            MinigameWon();
        }
    }

    public void MinigameWon()
    {
        Debug.Log("Good name");
        manager.minigameWon = true;
    }  
}
