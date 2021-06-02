using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject monster;
    public GameObject gameManager;

    private enum Ressource { RECONDITE, SODA, MEAT, WEED, FUNGUS, PURPLE_CRISTAL};

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

    // Start is called before the first frame update
    void Start()
    {
        NewMonster();
        UpdateTxtRessources();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTxtRessources()
    {
            reconditeTxt.GetComponent<UnityEngine.UI.Text>().text = (recondite - Count(Ressource.RECONDITE)).ToString();
            sodaTxt.GetComponent<UnityEngine.UI.Text>().text = (soda - Count(Ressource.SODA)).ToString();
            fungusTxt.GetComponent<UnityEngine.UI.Text>().text = (fungus - Count(Ressource.FUNGUS)).ToString();       
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
                        m.health += 1;
                        break;

                    case Ressource.SODA:
                        m.movementSpeed += 1;
                        break;
                }
            }

            NewMonster();
        }
        
    }

    void AddItem(Ressource ressource)
    {
        if (monsterWaiting.Count >= 3) monsterWaiting.Dequeue();
        monsterWaiting.Enqueue(ressource);
        UpdateTxtRessources();
    }

    public void AddRecondite()
    {
        AddItem(Ressource.RECONDITE);
    }

    public void AddSoda()
    {
        AddItem(Ressource.SODA);
    }

    public void AddFungus()
    {
        AddItem(Ressource.FUNGUS);
    }

    public void AddWeed()
    {
        AddItem(Ressource.WEED);
    }

    public void AddPurpleCristal()
    {
        AddItem(Ressource.PURPLE_CRISTAL);
    }

    public void AddMeat()
    {
        AddItem(Ressource.MEAT);
    }
}
