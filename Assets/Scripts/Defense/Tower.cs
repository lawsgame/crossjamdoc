using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Tower : MonoBehaviour, ITickable
{
    public enum TowerMode { SIMPLE, EXPLODING, CHANNELING, PERCING };
    public enum TargetMode { CLOSEST, WEAKEST, STRONGEST, HEALTHIEST, HARMEST, FIRST, RANDOM }

    public int damage = 3;
    public bool destroyable = false;
    public float radius = 1;
    
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
                case TargetMode.HARMEST:
                    target = affectedMonsters.OrderBy(m => m.health).First();
                    break;
                case TargetMode.HEALTHIEST:
                    target = affectedMonsters.OrderByDescending(m => m.health).First();
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
                    DrawLazer(transform, target.transform);
                    target.GetHurt(damage);
                    break;

                case TowerMode.EXPLODING:
                    RaycastHit2D[] hits;
                    Monster monster;
                    hits = Physics2D.CircleCastAll(target.transform.position, radius, Vector2.right);
                    foreach(RaycastHit2D hit in hits)
                    {
                        monster = hit.collider.transform.GetComponent<Monster>();
                        if(monster != null)
                        {
                            DrawLazer(transform, target.transform);
                            monster.GetHurt(damage);
                        }
                    }

                    target.GetHurt(damage);
                    break;

                case TowerMode.PERCING:
                    RaycastHit2D[] hitc; 
                    hitc = Physics2D.LinecastAll(transform.position, target.transform.position);
                    foreach (RaycastHit2D hit in hitc)
                    {
                        monster = hit.collider.transform.GetComponent<Monster>();
                        if (monster != null)
                        {
                            DrawLazer(transform, target.transform);
                            monster.GetHurt(damage);
                        }
                    }
                    target.GetHurt(damage);
                    DrawLazer(transform, hitc[hitc.Length-1].transform);

                    break;

                case TowerMode.CHANNELING:
                    List<Monster> knownMonsters = new List<Monster>
                    {
                        target
                    };
                    RaycastHit2D[] hitz;
                    DrawLazer(transform, target.transform);
                    for (int i = 0; i< knownMonsters.Count; i++)
                    {
                        hitz = Physics2D.CircleCastAll(knownMonsters[i].transform.position, radius, Vector2.right);
                        foreach (RaycastHit2D hit in hitz)
                        {
                            monster = hit.collider.transform.GetComponent<Monster>();
                            if (monster != null && !knownMonsters.Contains(monster))
                            {
                                DrawLazer(knownMonsters[i].transform, monster.transform);
                                knownMonsters.Add(monster);
                                monster.GetHurt(damage);
                            }
                            hit.collider.transform.GetComponent<Monster>()?.GetHurt(damage);
                        }
                    }
                    target.GetHurt(damage);
                    
                    break;
            }
        }
    }

    public void DrawLazer(Transform from, Transform to)
    {

    }
}
