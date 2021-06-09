using System;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int carryCapacity;
    public int lineOfSight;
    public int movementSpeed;
    public int strength;
    public int health;
    public int carried;

    MonsterMovement Movement { get; set; }

    AudioSource audioSource;

    void Start()
    {
        transform.tag = "Player";
        health = maxHealth;
        carried = 0;
        Movement = GetComponent<MonsterMovement>();
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(WorldManager.INSTANCE.tracks[4]);
    }

    public void GetHurt(int damage)
    {
        health -= damage;
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(WorldManager.INSTANCE.tracks[7]);
        if (health > maxHealth) health = maxHealth;
        else if (health <= 0) Die();
    }

    
    

    public void Die()
    {
        Debug.Log("dead");
        Destroy(gameObject);
    }

    public bool CanCarryMore() => carryCapacity > carried;
    public void InscrementCarryAmount() => carried++;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {

        if (otherCollider.gameObject.tag.Equals("PickableResource"))
        {
            if (CanCarryMore())
            {
                PickableResource pickableResource = otherCollider.gameObject.GetComponent<PickableResource>();
                GameObject.Destroy(otherCollider.gameObject);
                InscrementCarryAmount();

                //TODO: add factory receiving the resources

                Debug.Log("Resource picked up: " + otherCollider.gameObject.name);
            }
            else
            {
                Debug.Log("Monster cannot carry more resources ");
            }

        }
    }
}
