using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapSpike : MonoBehaviour, ITickable
{
    [SerializeField]
    readonly int damage = 1;
    readonly List<Monster> affectedMonsters = new List<Monster>();

    public void Start()
    {
        WorldManager.INSTANCE.tick.AddListener(Tick);
    }

    public void OnDestroy()
    {
        WorldManager.INSTANCE.tick.RemoveListener(Tick);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("collide");
        if(collision.transform.CompareTag("Player"))
        {
            affectedMonsters.Add(collision.transform.GetComponent<Monster>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            affectedMonsters.Remove(collision.transform.GetComponent<Monster>());
        }
    }

    public void Tick()
    {
        Monster[] monsters = affectedMonsters.ToArray();
        //ecarter les monstres qui pourraient devenir null pendant un foreach
        affectedMonsters.RemoveAll(x => x.health <= damage);
        for(int i = 0; i< monsters.Length; i++)
        {
            monsters[i].GetHurt(damage);
        } 
    }
}
