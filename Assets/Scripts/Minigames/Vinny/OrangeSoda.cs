using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSoda : MonoBehaviour
{
    [SerializeField] private GameInfoManager manager;
    [SerializeField] private float timer;

    [SerializeField] private int difficulty;

    [SerializeField] private bool win;
    [SerializeField] private bool fail;

    [SerializeField] private Animator minigameAnim;
    [SerializeField] private GameObject[] faces;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        minigameAnim.Play(difficulty.ToString());
        minigameAnim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            minigameAnim.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            minigameAnim.enabled = false;
        }
    }
}
