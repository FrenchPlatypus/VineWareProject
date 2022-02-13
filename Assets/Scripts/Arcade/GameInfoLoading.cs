using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoLoading : MonoBehaviour
{
    [SerializeField] private Sprite noGameVisual;

    [Header("Info Panel references")]
    [SerializeField] private Image gameInfoSprite;
    [SerializeField] private TextMeshProUGUI gameInfosTitle;
    [SerializeField] private TextMeshProUGUI gameInfosDescription;

    [SerializeField] private TextMeshProUGUI[] scoreTexts;

    public GameScriptableObject gameLoaded;

    // Load game infos from scriptableObject
    public void LoadGameInfos(GameScriptableObject gameSB)
    {
        int[] highscores = new int[3];

        gameLoaded = gameSB;

        gameInfoSprite.sprite = gameSB.visual;

        gameInfosTitle.text = gameSB.title;
        gameInfosDescription.text = gameSB.description;

        //Get highscores
        for (int i = 0; i < SaveManager.Instance._saveInfos.arcadeInfos.Count; i++)
        {
            if (SaveManager.Instance._saveInfos.arcadeInfos[i].title == gameSB.title)
            {
                if(SaveManager.Instance._saveInfos.arcadeInfos[i].scores != null && SaveManager.Instance._saveInfos.arcadeInfos[i].scores.Count == 3)
                {
                    highscores = SaveManager.Instance._saveInfos.arcadeInfos[i].scores.ToArray();
                }
                break;
            }
        }

        //Display highscores
        for(int i = 0; i < highscores.Length; i++)
        {
            scoreTexts[i].text = highscores[i].ToString();
        }

        //Set game sb in the game manager
        SaveManager.Instance.gameToLoad = gameSB;
    }

    // Reset panel infos to base values
    private void OnDisable()
    {
        gameInfoSprite.sprite = noGameVisual;

        gameInfosTitle.text = "";
        gameInfosDescription.text = "";
    }

    public void LoadMinigame()
    {

    }
}
