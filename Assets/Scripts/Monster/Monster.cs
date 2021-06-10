using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Factory;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int carryCapacity;
    public int lineOfSight;
    public int movementSpeed;
    public int strength;
    public int health;
    [SerializeField] private int carriedAmount;

    public event Action<Ressource,int> PickResource;
    

    MonsterMovement Movement { get; set; }

    AudioSource audioSource;

    void Start()
    {
        transform.tag = "Player";
        health = maxHealth;
        carriedAmount = 0;
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

    public bool CanCarryMore() => carryCapacity > carriedAmount;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PickableResource pickableResource = otherCollider.gameObject.GetComponent<PickableResource>();
        if (pickableResource != null)
        {
            if (CanCarryMore())
            {
                GameObject.Destroy(otherCollider.gameObject);
                carriedAmount++;
                Debug.Log("Resource picked up: " + pickableResource.Type);
                PickResource?.Invoke(pickableResource.Type, 1);
            }
            else
                Debug.Log("Monster cannot carry more resources ");
        }

        Mines mines = otherCollider.gameObject.GetComponent<Mines>();
        if (mines != null)
        {
            if (CanCarryMore())
            {

                int takenAmount = Math.Min(carryCapacity - carriedAmount, mines.Quantity);
                mines.ChangeQuantity(-takenAmount);
                carriedAmount += takenAmount;
                Debug.Log("Resource gotten from mines : " + mines.Type+" ("+ takenAmount + ")");
                PickResource?.Invoke(mines.Type, takenAmount);
                gameObject.SetActive(false);
            }
            else
                Debug.Log("Monster cannot carry more resources ");
        }
    }
}
