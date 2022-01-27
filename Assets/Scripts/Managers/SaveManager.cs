using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : SingletonMB<SaveManager>
{
    [Header("General game infos")]
    [SerializeField]
    public SaveInfos _saveInfos;

    public int storyCount;
    public int gameCount;

    [Header("Arcade infos")]
    public GameScriptableObject gameToLoad;

    // Start is called before the first frame update
    void Start()
    {
        GetSaveFile();
    }

    public void GetSaveFile()
    {
        Debug.Log(Application.persistentDataPath + "/savefile.save");

        SaveInfos infos = new SaveInfos();

        infos.storyInfos = new List<StoryInfos>();
        infos.arcadeInfos = new List<MinigameInfos>();

        if (!File.Exists(Application.persistentDataPath + "/savefile.save"))
        {
            Debug.Log("no save file found, create a new one");
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/ScriptableObjects/Minigames");

            // Add a story info for each minigame Directory
            storyCount = dir.GetDirectories().Length;
            for (int i = 0; i < dir.GetDirectories().Length; i++)
            {
                StoryInfos strInfo = new StoryInfos();
                strInfo.title = dir.GetDirectories()[i].Name;

                infos.storyInfos.Add(strInfo);

                // Get gamecount in each minigame Directory
                gameCount += dir.GetDirectories()[i].GetFiles().Length / 2;
                for(int j = 0; j < dir.GetDirectories()[i].GetFiles().Length; j ++)
                {
                    if (!dir.GetDirectories()[i].GetFiles()[j].Name.Contains(".meta"))
                    {
                        MinigameInfos mngInfo = new MinigameInfos();
                        string fileName = dir.GetDirectories()[i].GetFiles()[j].Name;

                        string[] parts = fileName.Split('.');
                        mngInfo.title = parts[0];

                        infos.arcadeInfos.Add(mngInfo);
                    }
                }
            }

            _saveInfos = infos;

            SaveDatas();
        }
        else
        {
            Debug.Log("save file found");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefile.save", FileMode.Open);
            _saveInfos = (SaveInfos)bf.Deserialize(file);
            file.Close();

            storyCount = _saveInfos.storyInfos.Count;
            gameCount = _saveInfos.arcadeInfos.Count;
        }
    }

    public void SaveDatas()
    {
        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savefile.save");
        bf.Serialize(file, _saveInfos);
        file.Close();
    }

    [System.Serializable]
    public class SaveInfos
    {
        public List<StoryInfos>storyInfos;

        public List<MinigameInfos> arcadeInfos;
    }

    [System.Serializable]
    public class StoryInfos
    {
        public string title;
        public bool storyCleared;
        public List<int> scores;
    }

    [System.Serializable]
    public class MinigameInfos
    {
        public string title;
        public bool unlocked;
        public List<int> scores;

    }
}

