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
    LineRenderer Laser;
    Vector3 mobOffset = new Vector3(0, 0.3f, -1);
    Vector3 towerOffset = new Vector3(0, 1.3f, -1);

    public void Start()
    {
        WorldManager.INSTANCE.tick.AddListener(Tick);
        Random.InitState(35354332);
        Laser = GetComponentInChildren<LineRenderer>();
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
            //if (Laser != null) Laser.enabled = false;
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

            List<Monster> damagedMonsters = new List<Monster>();
            switch (towerMode)
            {
                case TowerMode.SIMPLE:
                    damagedMonsters.Add(target);
                    break;

                case TowerMode.EXPLODING:
                    RaycastHit2D[] hits;
                    Monster monster;
                    hits = Physics2D.CircleCastAll(target.transform.position, radius, Vector2.right);
                    foreach (RaycastHit2D hit in hits)
                    {
                        monster = hit.collider.transform.GetComponent<Monster>();
                        if (monster != null)
                        {
                            damagedMonsters.Add(monster);
                        }
                    }
                    break;

                case TowerMode.PERCING:
                    RaycastHit2D[] hitc;
                    hitc = Physics2D.LinecastAll(transform.position, target.transform.position);
                    foreach (RaycastHit2D hit in hitc)
                    {
                        monster = hit.collider.transform.GetComponent<Monster>();
                        if (monster != null)
                        {
                            damagedMonsters.Add(monster);
                        }
                    }
                    break;

                case TowerMode.CHANNELING:
                    RaycastHit2D[] hitz;
                    for (int i = 0; i < damagedMonsters.Count; i++)
                    {
                        hitz = Physics2D.CircleCastAll(damagedMonsters[i].transform.position, radius, Vector2.right);
                        foreach (RaycastHit2D hit in hitz)
                        {
                            monster = hit.collider.transform.GetComponent<Monster>();
                            if (monster != null && !damagedMonsters.Contains(monster))
                            {
                                damagedMonsters.Add(monster);
                            }
                        }
                    }
                    break;
            }

            DrawLazer(damagedMonsters.Select(x => x.transform.position + mobOffset).ToList());
            damagedMonsters.ForEach(x => x.GetHurt(damage));
        }
    }

    public void DrawLazer(List<Vector3> positions)
    {
        if (Laser != null)
        {
            if (positions.Count != 0)
            {
                positions.Insert(0, transform.position + towerOffset);
                Laser.positionCount = positions.Count;
                Laser.SetPositions(positions.ToArray());
            }
            else
            {
                Laser.positionCount = 1;
            }
        }
    }
}
