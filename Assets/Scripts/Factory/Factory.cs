using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public GameObject monster;
    public GameObject gameManager;

    public enum Ressource { RECONDITE, SODA, MEAT, WEED, FUNGUS, PURPLE_CRISTAL};

    private Queue<Ressource> monsterWaiting;

    public GameObject reconditeTxt;
    public GameObject sodaTxt;
    public GameObject meatTxt;
    public GameObject weedTxt;
    public GameObject fungusTxt;
    public GameObject purpleCristalTxt;

    public int recondite;
    public int soda;
    public int meat;
    public int weed;
    public int fungus;
    public int purpleCristal;

    public GameObject pannel;
    

    // Start is called before the first frame update
    void Start()
    {
        NewMonster();
    }

    void UpdateTxtRessources()
    {
        reconditeTxt.GetComponent<Text>().text = (recondite - Count(Ressource.RECONDITE)).ToString();
        sodaTxt.GetComponent<Text>().text = (soda - Count(Ressource.SODA)).ToString();
        fungusTxt.GetComponent<Text>().text = (fungus - Count(Ressource.FUNGUS)).ToString();       
    }

    private int Count(Ressource ressource)
    {
        int val = 0;
        foreach (Ressource r in monsterWaiting)
        {
            if (r.Equals(ressource)) val++;
        }
        return val;
    }

    public void NewMonster()
    {
        monsterWaiting = new Queue<Ressource>();
        UpdateTxtRessources();
    }

    public void SpawnMonster()
    {
        if (monsterWaiting.Count >= 3 || true)
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
                    case Ressource.RECONDITE:
                        recondite--;
                        break;

                    case Ressource.FUNGUS:
                        fungus--;
                        m.maxHealth += 1;
                        m.health += 1;
                        break;

                    case Ressource.SODA:
                        soda--;
                        m.movementSpeed += 1;
                        break;
                }
            }
            NewMonster();
            pannel.GetComponent<RessourceBuffer>().UpdateMonsterWaitingBuffer(monsterWaiting);
        }
        
    }

    void AddItem(Ressource ressource)
    {
        if (monsterWaiting.Count >= 3) monsterWaiting.Dequeue();
        monsterWaiting.Enqueue(ressource);

        pannel.GetComponent<RessourceBuffer>().UpdateMonsterWaitingBuffer(monsterWaiting);

        UpdateTxtRessources();
       
    }

    public void AddRecondite()
    {
        if (recondite - Count(Ressource.RECONDITE) > 0) AddItem(Ressource.RECONDITE);
    }

    public void AddSoda()
    {
        if (soda - Count(Ressource.SODA) > 0) AddItem(Ressource.SODA);
    }

    public void AddFungus()
    {
        if (fungus - Count(Ressource.FUNGUS) > 0) AddItem(Ressource.FUNGUS);
    }

    public void AddWeed()
    {
        if (weed - Count(Ressource.WEED) > 0) AddItem(Ressource.WEED);
    }

    public void AddPurpleCristal()
    {
        if (purpleCristal - Count(Ressource.PURPLE_CRISTAL) > 0) AddItem(Ressource.PURPLE_CRISTAL);
    }

    public void AddMeat()
    {
        if (meat - Count(Ressource.MEAT) > 0) AddItem(Ressource.MEAT);
    }
}
