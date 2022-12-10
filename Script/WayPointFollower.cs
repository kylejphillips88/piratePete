using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    public int currentWayPointIndex = 0;
    public bool enemyAlive = true;
    public bool isEnemy = true;
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (isEnemy == true)
        {
            EnemyActions enemy = gameObject.GetComponent<EnemyActions>();
            enemy.EnemyRun();
            if (enemyAlive == false)
            { return; }
        }
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}
