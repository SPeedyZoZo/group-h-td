using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public float maxHealth;
    private float health;

    public GameObject innerCube;
    public UnityEngine.Color maxHealthColor;
    public UnityEngine.Color minHealthColor;

    private Material material;

    private void Start()
    {
        health = maxHealth;
        material = innerCube.GetComponent<Renderer>().material;
    }

    void Update()
    {
        UnityEngine.Color color = UnityEngine.Color.Lerp(minHealthColor, maxHealthColor, health / maxHealth);
        material.color = color;
        material.SetColor("_EmissionColor", color);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();

        if (enemy.damage < health)
            health -= enemy.damage;
        else
            Die();

        enemy.Die();
    }

    private void Die()
    {
        GameState.lives--;
        if (GameState.lives > 0)
            SceneManager.LoadScene("RetryLevel");
        else
            SceneManager.LoadScene("GameOver");
    }
}
