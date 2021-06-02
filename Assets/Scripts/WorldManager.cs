using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WorldManager : MonoBehaviour
{
    public static WorldManager INSTANCE;

    public UnityEvent tick;

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

        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            tick.Invoke();
        }
    }
}
