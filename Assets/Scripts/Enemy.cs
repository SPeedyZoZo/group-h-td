using UnityEngine;
using System.Collections;

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

    private Transform[] path;
    private int pathIndex = -1;

    public delegate void DealDamage(float damage);
    public static event DealDamage onDealDamage;

    public delegate void Death();
    public static event Death onDeath;

    public GameObject moneyStashPrefab; 


    void Start()
    {
        health = maxHealth;
    }

    public void Seek(Transform[] path)
    {
        this.path = path;
        pathIndex = 0;
    }

    void Update() 
    {
        if (pathIndex != -1)
        {
            Transform target = path[pathIndex];
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= distanceToTurn)
            {
                pathIndex++;
                if (pathIndex >= path.Length)
                {
                    onDealDamage(damage);
                    Destroy(gameObject);
                }
            }
        }

        Color color = Color.Lerp(minHealthColor, maxHealthColor, health / maxHealth);
        gameObject.GetComponent<Renderer>().material.color = color;
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
        GameObject moneyStash = Instantiate(moneyStashPrefab, transform.position, Quaternion.identity);

        MoneyStash moneyStashScript = moneyStash.GetComponent<MoneyStash>();
        moneyStashScript.moneyAmount = reward;

        onDeath();
        AudioManager.PlayEffect(deathSound, transform.position);
        Destroy(gameObject);
    }


    // SKILLS
    public void ApplySlowEffect(float duration, float speedMultiplier)
    {
        StartCoroutine(SlowEffect(duration, speedMultiplier));
    }

    private IEnumerator SlowEffect(float duration, float speedMultiplier)
    {
        float originalSpeed = speed;
        speed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
    }
}
