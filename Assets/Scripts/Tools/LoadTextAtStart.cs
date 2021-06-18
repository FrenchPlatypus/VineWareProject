using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTextAtStart : MonoBehaviour
{
    [TextArea]
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WriteTextPerSecond());
    }

    public IEnumerator WriteTextPerSecond()
    {
        for(int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<Text>().text += text[i].ToString();
        }
    }
}
