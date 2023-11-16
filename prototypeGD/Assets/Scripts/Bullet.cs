using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 45f;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
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
        GameObject effectsInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectsInstance, 2f);


        Destroy(target.gameObject);
        Destroy(gameObject);



    }

}
