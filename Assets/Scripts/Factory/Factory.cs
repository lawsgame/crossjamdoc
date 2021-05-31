using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject monster;
    public GameObject gameManager;

    private int[] currentRessourceMonster;
    private Queue<int[]> monstersWaiting;

    public int recondite;
    public int movementSpeedRessource;
    public int hitPointsRessource;
    public int carryCapacityRessource;
    public int lineOfSightRessource;
    public int strengthRessource;

    // Start is called before the first frame update
    void Start()
    {
        monstersWaiting = new Queue<int[]>();
        CreateRessourceMonster();

    }

    void CreateRessourceMonster()
    {
        currentRessourceMonster = new int[6] { 0, 0, 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createMonster()
    {
        if (currentRessourceMonster[0]>0)
        {
            monstersWaiting.Enqueue(currentRessourceMonster);
            CreateRessourceMonster();
        }
    }

    public void spawnMonster()
    {
        if (monstersWaiting.Count > 0)
        {
            GameObject gjm = (GameObject)Instantiate(monster);
            gjm.GetComponent<MonsterMovement>().GameManager = gameManager;
            gjm.GetComponent<MonsterMovement>().initialize();
            gjm.GetComponent<MonsterMovement>().StartMoving();

            Monster m = gjm.GetComponent<Monster>();
            m.movementSpeed += monstersWaiting.Peek()[1];
            m.hitPoints += monstersWaiting.Peek()[2];
            m.carryCapacity += monstersWaiting.Peek()[3];
            m.lineOfSight += monstersWaiting.Peek()[4];
            m.strength += monstersWaiting.Peek()[5];

            monstersWaiting.Dequeue();
        }
        
    }

    public void addRecondite()
    {
        currentRessourceMonster[0] += 1;
    }

    public void addMovementSpeed()
    {
        currentRessourceMonster[1] += 1;
    }

    public void addHitPoints()
    {
        currentRessourceMonster[2] += 1;
    }

    public void addCarryCapacity()
    {
        currentRessourceMonster[3] += 1;
    }

    public void addLineOfSight()
    {
        currentRessourceMonster[4] += 1;
    }

    public void addStrength()
    {
        currentRessourceMonster[5] += 1;
    }
}
