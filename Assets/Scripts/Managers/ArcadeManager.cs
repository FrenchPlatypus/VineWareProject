using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeManager : MonoBehaviour
{
    [SerializeField] private GameInfoManager gameManager;

    [Header("Minigame Variables")]
    public bool endlessMode;
    public List<minigame> minigames;
    public bool minigameOn;

    public statInfos infos;
    public Text levelText;

    [Header("Timer Variables")]
    public GameObject TimerParent;
    public Transform timerFiller;
    public float timerMultiplicator;

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
        gameManager = GetComponent<GameInfoManager>();

        GetHighScores();

        infos.difficulty = 0;
        infos.level = 1;
        infos.lives = 4;

        levelText.text = infos.level.ToString();

        minigame arcadeGames = new minigame();
        arcadeGames.stages = new GameObject[3];

        arcadeGames.stages = SaveManager.Instance.gameToLoad.gamePrefabs;

        minigames.Add(arcadeGames);

        StartCoroutine(WaitForIntro());
    }

    public void GetHighScores()
    {
        highScores = new int[3];

        // Get highscores from save manager
        for (int i = 0; i < SaveManager.Instance._saveInfos.arcadeInfos.Count; i++)
        {
            if(SaveManager.Instance._saveInfos.arcadeInfos[i].title == SaveManager.Instance.gameToLoad.title)
            {
                if(SaveManager.Instance._saveInfos.arcadeInfos[i].scores != null && SaveManager.Instance._saveInfos.arcadeInfos[i].scores.Count == 3)
                    highScores = SaveManager.Instance._saveInfos.arcadeInfos[i].scores.ToArray();           
                else
                    highScores = new int[3];

                break;
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
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadNewMinigame());
    }

    public IEnumerator CheckMinigameResult()
    {
        minigameOn = false;

        if (gameManager.minigameWon == true)
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
            if (bossBattle == false || gameManager.minigameWon == true)
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
        // Check if we need to update the highscores
        for (int i = 0; i < highScores.Length; i++)
        {
            if (infos.level > highScores[i])
            { 
                highScores[i] = infos.level;   

                highScoreTexts[i].text = infos.level.ToString();
                break;
            }
        }

        // Set highscores in save manager and save them
        for (int i = 0; i < SaveManager.Instance._saveInfos.arcadeInfos.Count; i++)
        {
            if (SaveManager.Instance._saveInfos.arcadeInfos[i].title == SaveManager.Instance.gameToLoad.title)
            {
                SaveManager.Instance._saveInfos.arcadeInfos[i].scores = highScores.ToList();
                SaveManager.Instance.SaveDatas();
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
        timerMultiplicator = 1682 / gameManager.maxTimer;

        TimerParent.SetActive(true);

        minigameOn = true;
        gameManager.minigameWon = false;
    }

    private void Update()
    {
        if (minigameOn == true)
        {
            if (gameManager.minigameTimer > 0)
            {
                if (bossBattle == false)
                {
                    gameManager.minigameTimer -= Time.deltaTime;
                    float fillAmount = gameManager.minigameTimer * timerMultiplicator;
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

        if (gameManager.minigameTimer > 0)
        {
            float timeText = Mathf.Round(gameManager.minigameTimer * 1f);
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
