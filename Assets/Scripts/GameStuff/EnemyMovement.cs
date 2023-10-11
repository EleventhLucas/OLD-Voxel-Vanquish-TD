using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    [HideInInspector]
    public int waypointIndex = 0;

    private Enemy enemy;
    public Vector3 offSet;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - (transform.position + offSet);
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance((transform.position + offSet), target.position) <= 0.2f)
        {
            GetNextWaypoint();
            transform.LookAt(target.position);
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndPath()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        PlayerStats.Lives--;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }
}
