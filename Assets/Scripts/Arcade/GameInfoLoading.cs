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

    public GameScriptableObject gameLoaded;

    // Load game infos from scriptableObject
    public void LoadGameInfos(GameScriptableObject gameSB)
    {
        gameLoaded = gameSB;

        gameInfoSprite.sprite = gameSB.visual;

        gameInfosTitle.text = gameSB.title;
        gameInfosDescription.text = gameSB.description;

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
