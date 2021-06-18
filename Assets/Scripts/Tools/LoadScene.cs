using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;

    public bool timed;
    public bool blockTimer;
    public int timer;

    public bool loadWithKey;
    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        if(timed == true)
        {
            StartCoroutine(LoadSceneWithTimer());
        }
    }

   public IEnumerator LoadSceneWithTimer()
    {
        while(blockTimer == true)
        {
            yield return null;
        }

        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(sceneToLoad);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void LoadSceneWithButton(string sceneName)
    {
        sceneToLoad = sceneName;
        StartCoroutine(LoadSceneWithTimer());
    }
}
