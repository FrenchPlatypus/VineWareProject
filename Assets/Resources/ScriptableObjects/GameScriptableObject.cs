using UnityEngine;
using System.Collections;
using UnityEditor;



[CreateAssetMenu(fileName = "Microgame", menuName = "ScriptableObjects/Microgames", order = 1)]
public class GameScriptableObject : ScriptableObject
{
    public string title;
    [TextArea(5,20)]
    public string description;

    [TextArea(5, 20)]
    public string source;

    public GameObject[] gamePrefabs;
    public Sprite visual;
}