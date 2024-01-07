using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy target;

    public float speed = 45f;
    public float damage = 10f;

    public GameObject impactEffect;

    public void Seek(Enemy _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // this might be a race condition?
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) // if true then target is hit
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        target.TakeDamage(damage);

        GameObject effectsInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectsInstance, 2f);

        Destroy(gameObject);
    }
}
