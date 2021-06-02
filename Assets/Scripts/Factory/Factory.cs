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

    public void CancelMonster()
    {
        movementSpeedRessource += currentRessourceMonster[1];
        hitPointsRessource += currentRessourceMonster[2];
        carryCapacityRessource += currentRessourceMonster[3];
        lineOfSightRessource += currentRessourceMonster[4];
        strengthRessource += currentRessourceMonster[5];
        CreateRessourceMonster();
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
        if (recondite > 0)
        {
            recondite -= 1;
            currentRessourceMonster[0] += 1;
        }
    }

    public void addMovementSpeed()
    {
        if (movementSpeedRessource > 0)
        {
            movementSpeedRessource -= 1;
            currentRessourceMonster[1] += 1;
        }
    }

    public void addHitPoints()
    {
        if (hitPointsRessource > 0)
        {
            hitPointsRessource -= 1;
            currentRessourceMonster[2] += 1;
        }
    }

    public void addCarryCapacity()
    {
        if (carryCapacityRessource > 0)
        {
            carryCapacityRessource -= 1;
            currentRessourceMonster[3] += 1;
        }
    }

    public void addLineOfSight()
    {
        if (lineOfSightRessource > 0)
        {
            lineOfSightRessource -= 1;
            currentRessourceMonster[4] += 1;
        }
    }

    public void addStrength()
    {
        if (strengthRessource > 0)
        {
            strengthRessource -= 1;
            currentRessourceMonster[5] += 1;
        }
    }
}
