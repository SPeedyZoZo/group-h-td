using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 45f;
    public float damage = 10f;
    public bool pierce = false;
    public GameObject impact;

    private void Start()
    {
        Invoke("Expire", 10f);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Expire()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Instantiate(impact, transform);
        DealDamage(collider);
        if (!pierce)
            Destroy(gameObject);
    }

    protected virtual void DealDamage(Collider collider)
    {
        Enemy target = collider.GetComponent<Enemy>();
        target.TakeDamage(damage);
    }
}
