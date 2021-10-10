using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public float timer;
    public AudioSource source;

    void OnEnable()
    {
        timer = source.clip.length;

        InvokeRepeating("Delete", timer, 1);
    }

    public void Delete()
    {
        Destroy(this);
    }
}
