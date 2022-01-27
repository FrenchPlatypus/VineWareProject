using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeManager : MonoBehaviour
{
    [Header("Minigame Variables")]
    public bool endlessMode;
    public List<minigame> minigames;
    public bool minigameOn;
    public bool minigameWon;

    public statInfos infos;
    public Text levelText;

    [Header("Timer Variables")]
    public GameObject TimerParent;
    public Transform timerFiller;
    public float timerMultiplicator;
    public float minigameTimer;
    public float maxTimer;

    public Text lifesNumber;
    public Transform minigameParent;
    public Animator canvasAnimator;

    [Header("Score Variables")]
    public int[] highScores;
    public Text[] highScoreTexts;
    public string scene;

    [Header("Sound Variables")]
    public AudioClip[] soundEffects;
    public Transform soundParent;
    public GameObject soundPrefab;

    public bool bossBattle;

    public void Start()
    {
        GetHighScores();

        infos.difficulty = 0;
        infos.level = 1;

        levelText.text = infos.level.ToString();

        minigame arcadeGames = new minigame();
        arcadeGames.stages = SaveManager.Instance.gameToLoad.gamePrefabs;

        minigames.Add(arcadeGames);

        StartCoroutine(WaitForIntro());
    }

    public void GetHighScores()
    {
        highScores = new int[6];
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.HasKey("highscore" + scene + i))
            {
                highScores[i] = PlayerPrefs.GetInt("highscore" + scene + i);
                highScoreTexts[i].text = PlayerPrefs.GetInt("highscore" + scene + i).ToString();
            }
        }
    }

    public void InstantiateSound(int sound)
    {
        GameObject soundInstance = Instantiate(soundPrefab, soundParent);
        soundInstance.GetComponent<AudioSource>().clip = soundEffects[sound];

        soundInstance.SetActive(true);
    }

    public IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("Intro finished");

        canvasAnimator.Play("NewMinigame");
        yield return new WaitForSeconds(2);
        StartCoroutine(LoadNewMinigame());
    }

    public IEnumerator CheckMinigameResult()
    {
        minigameOn = false;

        if (minigameWon == true)
        {
            InstantiateSound(2);
            canvasAnimator.Play("WinMinigame");
            yield return new WaitForSeconds(0.1f);
            Destroy(minigameParent.GetChild(0).gameObject);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            InstantiateSound(1);
            canvasAnimator.Play("LoseMinigame");
            yield return new WaitForSeconds(0.1f);
            Destroy(minigameParent.GetChild(0).gameObject);
            yield return new WaitForSeconds(1f);

            infos.lives--;
            lifesNumber.text = infos.lives.ToString();
        }

        if (infos.lives == 0)
        {
            Time.timeScale = 1;
            canvasAnimator.Play("GameOver");
            CheckForScore();
        }
        else
        {
            if (bossBattle == false || minigameWon == true)
            {
                infos.level++;
            }

            // Speed up every 3 games
            if (infos.level % 4 == 0)
            {
                Time.timeScale += 0.1f;

                canvasAnimator.Play("SpeedUp");
                yield return new WaitForSeconds(1);
            }

            // raise difficulty after each boss battle
            if (infos.level % 4 == 0)
            {
                infos.difficulty = 0;
                yield return new WaitForSeconds(1);
            }
            else
                infos.difficulty++;

            levelText.text = infos.level.ToString();
            InstantiateSound(0);
            canvasAnimator.Play("NewMinigame");
            yield return new WaitForSeconds(2);

            StartCoroutine(LoadNewMinigame());

        }
    }

    public void CheckForScore()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (infos.level > highScores[i])
            {
                PlayerPrefs.SetInt("highscore" + scene + i, infos.level);
                highScoreTexts[i].text = infos.level.ToString();
                break;
            }
        }
    }

    public IEnumerator LoadNewMinigame()
    {
        // Instantiate one of the minigames
        int rand = Random.Range(0, minigames.Count);
        Debug.Log("rolled " + rand);
        GameObject minigameObject = minigames[rand].stages[infos.difficulty];
        Instantiate(minigameObject, minigameParent);

        Debug.Log(minigameObject.name);

        yield return new WaitForSeconds(0.1f);

        timerFiller.localPosition = new Vector2(1682, 0);
        timerMultiplicator = 1682 / maxTimer;

        TimerParent.SetActive(true);

        minigameOn = true;
        minigameWon = false;
    }

    private void Update()
    {
        if (minigameOn == true)
        {
            if (minigameTimer > 0)
            {
                if (bossBattle == false)
                {
                    minigameTimer -= Time.deltaTime;
                    float fillAmount = minigameTimer * timerMultiplicator;
                    timerFiller.localPosition = new Vector2(fillAmount, 0);
                }
            }
            else
            {
                Debug.Log("Time out");
                TimerParent.SetActive(false);
                StartCoroutine(CheckMinigameResult());
            }
        }

        if (minigameTimer > 0)
        {
            float timeText = Mathf.Round(minigameTimer * 1f);
            TimerParent.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = timeText.ToString();
        }
    }

    [System.Serializable]
    public class statInfos
    {
        public int difficulty;
        public float speed;
        public int level;
        public int lives;
    }

    [System.Serializable]
    public class minigame
    {
        public string name;
        public GameObject[] stages;
    }
}
