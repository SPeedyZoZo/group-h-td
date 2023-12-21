using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int reward = 50;
    public float speed;
    public float damage;
    public float distanceToTurn;

    public float maxHealth;
    private float health;

    public Color maxHealthColor;
    public Color minHealthColor;

    public AudioClip deathSound;

    private Transform target;
    private int wavepointIndex = 0;

    public delegate void DealDamage(float damage);
    public static event DealDamage onDealDamage;

    public delegate void Death();
    public static event Death onDeath;

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
        onDealDamage(damage);
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
        onDeath();
        LevelManager.money += reward;
        AudioManager.PlayEffect(deathSound, transform.position);
        Destroy(gameObject);
    }
}
