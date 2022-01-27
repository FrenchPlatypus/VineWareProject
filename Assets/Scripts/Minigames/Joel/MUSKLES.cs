using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MUSKLES : MonoBehaviour
{
    public StoryModeManager manager;

    public float timer;
    public float progress;

    public Image background;

    public Transform musclePivot;
    public Sprite[] muscleSprites;

    public int muscleMultiplicator;

    // Min rot : 0; Max rot : 75

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StoryModeManager").GetComponent<StoryModeManager>();
        manager.minigameTimer = timer;
        manager.maxTimer = timer;

        InvokeRepeating("UpdateProgress", 0, 0.01f);
    }

    void UpdateProgress()
    {
        if (progress > 0 && progress < 100)
        {
            progress--;

        }
    }
    void Update()
    {
        if (progress > 0 && progress < 100)
        {
            float muscleRotation = (progress * 15) / 100;
            Debug.Log(muscleRotation);
            musclePivot.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, muscleRotation);

            if(progress < 25)
            {
                musclePivot.GetChild(0).GetComponent<Image>().sprite = muscleSprites[0];
            }
            else if(progress >= 25 && progress < 50)
            {
                musclePivot.GetChild(0).GetComponent<Image>().sprite = muscleSprites[1];
            }
            else if (progress >= 50 && progress < 75)
            {
                musclePivot.GetChild(0).GetComponent<Image>().sprite = muscleSprites[2];
            }
            else if (progress >= 75)
            {
                musclePivot.GetChild(0).GetComponent<Image>().sprite = muscleSprites[3];
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && progress < 100)
        {
            progress += muscleMultiplicator;
        }

        if (progress >= 100 && manager.minigameTimer > 0)
        {
            MinigameWon();
            CancelInvoke();
        }
    }

    public void MinigameWon()
    {
        manager.minigameWon = true;
        background.GetComponent<Animator>().Play("Win");
        musclePivot.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 15);

        if(manager.minigameTimer > 1)
        {
            manager.minigameTimer = 1;
        }
    }  
}
