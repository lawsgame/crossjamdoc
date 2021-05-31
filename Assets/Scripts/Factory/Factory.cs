using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject monster;
    public GameObject gameManager;
    private List<GameObject> monstersWaiting;

    public int recondite;
    public int speedRessource;
    public int healthRessource;
    public int seekRessource;
    public int visionRessource;
    public int strengthRessource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InstantiateMonster()
    {
        monstersWaiting.Add((GameObject) Instantiate(monster));
        Debug.Log(monstersWaiting.Count);
        monstersWaiting[0].GetComponent<Monster>().movementSpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnMonster()
    {
        InstantiateMonster();
    }
}
