using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range;
    [Range(0f, 1f)]
    public float accuracy = 1f;
    public float fireInterval = 1f;
    public float rotationSpeed = 15f;

    public GameObject bullet;
    public GameObject barrel;
    public AudioClip shootSound;

    private string targetTag = "Enemy";
    private GameObject target;
    private Animator animator;
    private float nextShootTime;

    private void Start()
    {
        nextShootTime = Time.time;
        animator = GetComponent<Animator>();
        InvokeRepeating("AcquireTarget", 0f, 0.2f);
    }

    private void AcquireTarget()
    {
        float shortestDist = range;
        GameObject closestTarget = null;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject curTarget in targets)
        {
            float dist = Vector3.Distance(transform.position, curTarget.transform.position);
            if (dist >= shortestDist)
                continue;

            shortestDist = dist;
            closestTarget = curTarget;
        }

        target = closestTarget;
    }

    private void Update()
    {
        if (!target)
            return;

        float step = rotationSpeed * Time.deltaTime;

        Vector3 directionWithoutHeight = new Vector3(
            target.transform.position.x - transform.position.x,
            0f,
            target.transform.position.z - transform.position.z
        );

        Quaternion rotation = Quaternion.LookRotation(directionWithoutHeight);
        Quaternion desiredRotation = Quaternion.RotateTowards(transform.rotation, rotation, 360f);
        Quaternion lerpRoation = Quaternion.RotateTowards(transform.rotation, rotation, step);
        transform.rotation = lerpRoation;

        float delta = Quaternion.Angle(lerpRoation, desiredRotation);

        // shoot if aimed at target and not on cooldown
        if (
            180f - delta >= accuracy * 180f && 
            nextShootTime <= Time.time)
        {
            AudioManager.PlayEffect(shootSound, transform.position);
            animator.Play("Shoot");

            Instantiate(bullet, barrel.transform.position, transform.rotation);
            nextShootTime = Time.time + fireInterval;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}