using UnityEngine;

public class SplashBullet : Bullet
{
    public float radius = 5f;
    public LayerMask targetLayer;

    protected override void DealDamage(Collider _)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        foreach (Collider collider in colliders)
        {
            Enemy target = collider.GetComponent<Enemy>();
            target.TakeDamage(damage);
        }
    }
}
