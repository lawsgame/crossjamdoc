using UnityEngine;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int carryCapacity;
    public int lineOfSight;
    public int movementSpeed;
    public int strength;
    public int health;

    void Start()
    {
        transform.tag = "Player";
        health = maxHealth;

    }

    public void GetHurt(int damage)
    {
        health -= damage;
        if (health > maxHealth) health = maxHealth;
        else if (health <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("dead");
        Destroy(gameObject);
    }
}
