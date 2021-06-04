using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Tower : MonoBehaviour, ITickable
{
    public enum TowerMode { SIMPLE, EXPLODING, CHANNELING, PERCING };
    public enum TargetMode { CLOSEST, WEAKEST, STRONGEST, FIRST, RANDOM }

    public int damage = 3;
    public bool destroyable = false;

    public bool targetClosest = false;
    public TowerMode towerMode = TowerMode.SIMPLE;
    public TargetMode targetMode = TargetMode.FIRST;

    readonly List<Monster> affectedMonsters = new List<Monster>();


    public void Start()
    {
        WorldManager.INSTANCE.tick.AddListener(Tick);
        Random.InitState(35354332);
    }

    public void OnDestroy()
    {
        WorldManager.INSTANCE.tick.RemoveListener(Tick);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("collide");
        if (collision.transform.CompareTag("Player"))
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
        if (affectedMonsters.Count > 0)
        {

            Monster target;
            switch (targetMode)
            {

                case TargetMode.CLOSEST:
                    target = affectedMonsters.OrderBy(m => Vector2.Distance(transform.position, m.transform.position)).First();
                    break;
                case TargetMode.FIRST:
                    target = affectedMonsters.OrderBy(m => m.movementSpeed).First();
                    break;
                case TargetMode.WEAKEST:
                    target = affectedMonsters.OrderBy(m => m.strength).First();
                    break;
                case TargetMode.STRONGEST:
                    target = affectedMonsters.OrderByDescending(m => m.strength).First();
                    break;
                case TargetMode.RANDOM:
                    int choice = Random.Range(0, affectedMonsters.Count);
                    target = affectedMonsters[choice];
                    break;
                default:
                    target = affectedMonsters[0];
                    break;
            }

            switch (towerMode)
            {
                case TowerMode.SIMPLE:
                    target.GetHurt(damage);
                    break;
                case TowerMode.EXPLODING:
                    
                    break;
                case TowerMode.PERCING:
                    break;
                case TowerMode.CHANNELING:
                    break;
            }
        }

    }
}
