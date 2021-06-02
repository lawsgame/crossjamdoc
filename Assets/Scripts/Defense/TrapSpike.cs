using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour, ITickable
{
    int damage = 1;

    List<Monster> affectedMonsters = new List<Monster>();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            affectedMonsters.Add(collision.transform.GetComponent<Monster>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            affectedMonsters.Remove(collision.transform.GetComponent<Monster>());
        }
    }

    public void Tick()
    {
        affectedMonsters.ForEach(x => x.GetHurt(damage));
    }
}
