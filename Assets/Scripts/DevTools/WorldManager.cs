using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldManager : MonoBehaviour
{
    public static WorldManager INSTANCE;

    public float tickSec = 0.2f;
    public UnityEvent tick;
    public List<AudioClip> tracks;

    AudioSource audioSource;

    void Start()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickSec);
            tick.Invoke();
        }
    }

}
