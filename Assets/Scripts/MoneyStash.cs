using UnityEngine;
using System.Collections;

using UnityEngine;

public class MoneyStash : MonoBehaviour
{
    public int moneyAmount;
    public float bounceHeight;
    public float bounceSpeed;
    public float dropRadius;

    private bool isCollected = false;
    private float initialYPosition;

    void Start()
    {
        initialYPosition = transform.position.y;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 dropPosition = transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * dropRadius;

        transform.position = new Vector3(dropPosition.x, initialYPosition, dropPosition.z);
    }

    void Update()
    {
        if (!isCollected)
        {
            float yOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            transform.position = new Vector3(transform.position.x, initialYPosition + yOffset, transform.position.z);
        }
    }

    void OnMouseOver()
    {
        if (!isCollected)
            CollectMoney();
    }

    void CollectMoney()
    {
        LevelManager.money += moneyAmount;
        isCollected = true;
        Destroy(gameObject);
    }
}