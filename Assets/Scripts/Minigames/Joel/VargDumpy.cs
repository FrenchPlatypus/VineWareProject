using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VargDumpy : MonoBehaviour
{
    [SerializeField] private GameInfoManager manager;
    [SerializeField] private float timer;

    [SerializeField] private int difficulty;

    [SerializeField] private bool end;
    [SerializeField] private bool open;

    [SerializeField] private Animator pizzaAnim;
    [SerializeField] private Animator handAnim;
    [SerializeField] private Transform fren;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameInfoManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        pizzaAnim.Play(difficulty.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !end)
        {
            end = true;
            StartCoroutine(StopWheel());
        }
    }

    public IEnumerator StopWheel()
    {
        handAnim.enabled = true;
        yield return new WaitForSeconds(0.12f);

        if (open)
            MinigameWon();
        else
        {
            fren.SetParent(this.transform.parent);
            fren.GetComponent<Rigidbody2D>().simulated = true;

            if (manager.minigameTimer > 1)
            {
                manager.minigameTimer = 1;
            }
        }

        pizzaAnim.enabled = false;
    }

    public void MinigameWon()
    {
        manager.minigameWon = true;

        if (manager.minigameTimer > 1)
        {
            manager.minigameTimer = 1;
        }
    }
}
