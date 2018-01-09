using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Unity setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Turret settings")]
    public float range = 3f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    private float fireCountdown = 0f;
    private Transform target = null;
    
	void Update () {
        if (target == null || !IsTargetInRange())
        {
            AcquireTarget();
        }
        else
        {
            var pos = target.position - transform.position;
            var newRot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, turnSpeed * Time.deltaTime);
            Attack(target);
        }
	}

    private void Attack(Transform target)
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.target = target;
    }

    /// <summary>
    /// Boucle sur les enemies in range et selectionne le plus avancé
    /// </summary>
    private void AcquireTarget()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, range);
        int nextWaypointIndex, maxWaypoint = 0;
        float distToWaypoint, minDistToWayoint = Mathf.Infinity;
        Enemy enemy;
        int enemiesInRange = 0;

        for (int i = 0, l=objectsInRange.Length;  i < l; i++)
        {
            enemy = objectsInRange[i].GetComponent<Enemy>();
            if (enemy)
            {
                enemiesInRange++;
                nextWaypointIndex = enemy.waypointIndex;
                distToWaypoint = enemy.distanceToWaypoint;
                if (nextWaypointIndex > maxWaypoint)
                {
                    maxWaypoint = nextWaypointIndex;
                    minDistToWayoint = distToWaypoint;
                    target = enemy.transform;
                }
                else if(nextWaypointIndex == maxWaypoint)
                {
                    if (distToWaypoint < minDistToWayoint)
                    {
                        minDistToWayoint = distToWaypoint;
                        target = enemy.transform;
                    }
                }
            }
        }
        if (enemiesInRange == 0)
            target = null;
    }

    private bool IsTargetInRange()
    {
        return target != null ? Vector3.Distance(transform.position, target.position) <= range : false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
