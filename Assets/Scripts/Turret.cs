using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 10f;
    public bool targetsFlyingEnemies = false;

    [Header("Use Bullets (defult)")]

    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float fireCountdown = 0f;

    [Header("Use Laser / effects.")]

    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;
    public Transform targetHitPoint;
   

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget ()
    {
        target = null;
        targetEnemy = null;
        int nearestWaypointIndex, colliderWaypointIndex;
        bool flying;
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(range, range, range), Quaternion.identity, (1 << 9)); // The 1 << 9 makes sure it's only enemy tagged objects
        if (colliders.Length != 0)
        {
            GameObject nearestEnemy = colliders[0].gameObject;
            nearestWaypointIndex = nearestEnemy.GetComponent<EnemyMovement>().waypointIndex;
            foreach (Collider collider in colliders)
            {
                flying = collider.gameObject.GetComponent<Enemy>().isFlying;
                colliderWaypointIndex = collider.gameObject.GetComponent<EnemyMovement>().waypointIndex;
                if ((nearestWaypointIndex < colliderWaypointIndex || nearestWaypointIndex == colliderWaypointIndex && Vector3.Distance(collider.gameObject.transform.position, Waypoints.points[colliderWaypointIndex].position) < Vector3.Distance(nearestEnemy.gameObject.transform.position, Waypoints.points[nearestWaypointIndex].position)) && (targetsFlyingEnemies || !flying))
                {
                    nearestWaypointIndex = colliderWaypointIndex;
                    nearestEnemy = collider.gameObject;
                }
            }
            if (targetsFlyingEnemies || !nearestEnemy.GetComponent<Enemy>().isFlying)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
                targetHitPoint = target.gameObject.GetComponent<Enemy>().hitPoint;
            }
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update () {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }

            }

            return;
        }

        //Target lock on
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        } else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f * fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

	}

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        //targetEnemy.Slow(slowAmount); Disabled for now.

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetHitPoint.position);

        Vector3 dir = firePoint.position - targetHitPoint.position;

        impactEffect.transform.position = targetHitPoint.position;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot ()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2, range * 2, range * 2));
    }
}
