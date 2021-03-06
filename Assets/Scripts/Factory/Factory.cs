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

    public GameObject SpawnButton;

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

    void UpdateSpawnButton()
    {

        SpawnButton.GetComponent<Button>().interactable = (monsterWaiting.Count >= 3);
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
        UpdateSpawnButton();
        pannel.GetComponent<RessourceBuffer>().UpdateMonsterWaitingBuffer(monsterWaiting);
    }

    public void SpawnMonster()
    {
        if (monsterWaiting.Count >= 3 || true)
        {
            GameObject gjm = (GameObject)Instantiate(monster);

            MonsterMovement mm = gjm.GetComponent<MonsterMovement>();
            mm.GameManager = gameManager;
            mm.Initialize();
            mm.ResetPosition();

            Monster m = gjm.GetComponent<Monster>();
            m.PickResource += addResourceToStock;

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

            gjm.GetComponent<MonsterMovement>().StartMoving();
        }
        
    }

    void AddItem(Ressource ressource)
    {
        if (monsterWaiting.Count >= 3) monsterWaiting.Dequeue();
        monsterWaiting.Enqueue(ressource);

        UpdateTxtRessources();
        UpdateSpawnButton();
        pannel.GetComponent<RessourceBuffer>().UpdateMonsterWaitingBuffer(monsterWaiting);       
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

    public void addResourceToStock(Ressource type, int quantity)
    {
        Debug.Log("Resource stockpilled :"+ quantity + " of "+type);
        switch (type)
        {
            case Ressource.FUNGUS: fungus += quantity; break;
            case Ressource.MEAT: meat += quantity; break;
            case Ressource.PURPLE_CRISTAL: purpleCristal += quantity; break;
            case Ressource.RECONDITE: recondite += quantity; break;
            case Ressource.SODA: soda += quantity; break;
            case Ressource.WEED: weed += quantity; break;
        }
        UpdateTxtRessources();
    }
}
