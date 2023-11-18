using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int moneyValue = 50;
    public float speed;
    public float distanceToTurn;

    public float maxHealth;
    private float health;

    public Color maxHealthColor;
    public Color minHealthColor;

    public AudioClip deathSound;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
        health = maxHealth;
    }

    void Update() 
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position)<= distanceToTurn) 
        {
            GetNextWaypoint();
        }

        Color color = Color.Lerp(minHealthColor, maxHealthColor, health / maxHealth);
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    void GetNextWaypoint() 
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) 
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Debug.Log(PlayerStats.Lives);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (damage < health)
            health -= damage;
        else
            Die();
    }

    void Die()
    {
        PlayerStats.Money += moneyValue; // Add the set amount of money to the player's balance
        Debug.Log("Enemy defeated! Money now --> " + PlayerStats.Money);

        // Play the death sound
        AudioSource.PlayClipAtPoint(deathSound, Vector3.zero);

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
