using System.Collections;
using UnityEngine;

public class TurretAOE : MonoBehaviour
{
    [Header("General")]

    public float range = 10f;

    public float fireRate = 2f;
    private float fireCountdown = 0f;

    public float slowAmount = .5f;

    public GameObject impactEffect;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform firePoint;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length != 0)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(range, range, range), Quaternion.identity,(1 << 9));
            if (fireCountdown <= 0 && colliders.Length != 0)
            {
                Shoot();
                fireCountdown = 1f * fireRate;
            }
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    collider.gameObject.GetComponent<Enemy>().Slow(slowAmount);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();

        //targetEnemy.TakeDamage(damageOverTime * Time.deltaTime); This is for mage
        fireCountdown -= Time.deltaTime;

    }

    private void Shoot ()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, firePoint.position, transform.rotation);
        Destroy(effectIns, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2, range * 2, range * 2));
    }
}
