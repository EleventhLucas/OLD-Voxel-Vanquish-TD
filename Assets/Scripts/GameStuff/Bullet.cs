using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    private Transform targetHitPoint;

    public float speed = 70f;

    public int damage = 10;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Seek (Transform _target)
    {
        target = _target;
        targetHitPoint = target.gameObject.GetComponent<Enemy>().hitPoint;
    }

	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = targetHitPoint.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetHitPoint);

	}

    void HitTarget ()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if (explosionRadius > 0f)
        {
            Explode();
            FindObjectOfType<AudioManager>().Play("Explosion");
        }   else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
        FindObjectOfType<AudioManager>().Play("EnemyHurt");

        e.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
