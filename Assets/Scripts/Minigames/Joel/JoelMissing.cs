using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoelMissing : MonoBehaviour
{
    public GameInfoManager manager;

    public GameObject[] questionSets;

    public int difficulty;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        for(int i = 0; i < questionSets[difficulty].transform.GetChild(1).childCount; i++)
        {
            int random = Random.Range(0, questionSets[difficulty].transform.GetChild(1).childCount -1);
            questionSets[difficulty].transform.GetChild(1).GetChild(i).SetSiblingIndex(random);
        }
        questionSets[difficulty].SetActive(true);

    }

    public void MinigameWon()
    {
        manager.minigameWon = true;
    }
}
