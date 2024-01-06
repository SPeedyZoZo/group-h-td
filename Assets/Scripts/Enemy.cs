using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int reward = 50;
    public float speed;
    public float damage;
    public float distanceToTurn;

    public float maxHealthEasy;
    public float maxHealthMedium;
    public float maxHealthHard;

    private float maxHealth;
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
        switch (GameState.difficulty)
        {
            case GameState.Difficulty.Easy:
                maxHealth = maxHealthEasy;
                break;
            case GameState.Difficulty.Medium:
                maxHealth = maxHealthMedium;
                break;
            case GameState.Difficulty.Hard:
                maxHealth = maxHealthHard;
                break;
        }

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

    public void ApplyBurningEffect(float duration, int damagePerTick)
    {
        StartCoroutine(BurningEffect(duration, damagePerTick));
    }

    private IEnumerator SlowEffect(float duration, float speedMultiplier)
    {
        float originalSpeed = speed;
        speed *= speedMultiplier;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
    }

    private IEnumerator BurningEffect(float duration, int damagePerTick)
    {
        float elapsedTime = 0f;
        float tickInterval = 1f; // Adjust this interval as needed

        while (elapsedTime < duration)
        {
            float damageThisTick = damagePerTick * tickInterval / duration;
            TakeDamage(damageThisTick);

            elapsedTime += tickInterval;
            yield return new WaitForSeconds(tickInterval);
        }
    }
}
