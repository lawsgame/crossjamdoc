using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject monster;
    public GameObject gameManager;
    private GameObject monstersWaiting;

    public int recondite;
    public int speedRessource;
    public int healthRessource;
    public int seekRessource;
    public int visionRessource;
    public int strengthRessource;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateMonster();
    }

    void InstantiateMonster()
    {
        GameObject gjm = (GameObject) Instantiate(monster);
        gjm.GetComponent<MonsterMovement>().GameManager = gameManager;
        monstersWaiting = gjm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnMonster()
    {
        monstersWaiting.GetComponent<MonsterMovement>().initialize();
        monstersWaiting.GetComponent<MonsterMovement>().StartMoving();
        InstantiateMonster();
    }

    public void addSpeed()
    {
        monstersWaiting.GetComponent<Monster>().movementSpeed += 2;
    }
}
