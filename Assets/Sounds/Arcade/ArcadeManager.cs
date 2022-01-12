using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeManager : MonoBehaviour
{
    public List<GameInfos> gameList;
    public Transform gameParent;
}

public class GameInfos
{
    public GameObject gamePrefab;
    public string title;
    public Sprite screen;
}
