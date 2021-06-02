using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject monster;
    public GameObject gameManager;

    private enum Ressource { RECONDITE, SODA, MEAT, WEED, FUNGUS, PURPLE_CRISTAL};

    private Queue<Ressource> monsterWaiting;

    public int recondite;
    public int soda;
    public int meat;
    public int weed;
    public int fungus;
    public int purpleCristal;

    // Start is called before the first frame update
    void Start()
    {
        NewMonster();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewMonster()
    {
        monsterWaiting = new Queue<Ressource>();
    }

    public void SpawnMonster()
    {
        if (monsterWaiting.Count >= 3)
        {
            GameObject gjm = (GameObject)Instantiate(monster);
            gjm.GetComponent<MonsterMovement>().GameManager = gameManager;
            gjm.GetComponent<MonsterMovement>().initialize();
            gjm.GetComponent<MonsterMovement>().StartMoving();

            Monster m = gjm.GetComponent<Monster>();

            foreach (Ressource ressource in monsterWaiting)
            {
                switch (ressource)
                {
                    case Ressource.FUNGUS:
                        m.maxHealth += 1;
                        break;

                    case Ressource.SODA:
                        m.movementSpeed += 1;
                        break;
                }
            }

            NewMonster();
        }
        
    }

    private void addItem(Ressource ressource)
    {
        if (monsterWaiting.Count >= 3) monsterWaiting.Dequeue();
        monsterWaiting.Enqueue(ressource);
    }

    public void addRecondite()
    {
        addItem(Ressource.RECONDITE);
    }

    public void addMovementSpeed()
    {
        addItem(Ressource.SODA);
    }

    public void addHitPoints()
    {
        addItem(Ressource.FUNGUS);
    }

    public void addCarryCapacity()
    {
        addItem(Ressource.WEED);
    }

    public void addLineOfSight()
    {
        addItem(Ressource.PURPLE_CRISTAL);
    }

    public void addStrength()
    {
        addItem(Ressource.MEAT);
    }
}
