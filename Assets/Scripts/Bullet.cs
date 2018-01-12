using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [Header("Bullet settings")]
    public float speed = 70f;

    [Header("Unity setup")]
    public GameObject impactEffect;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Turret turret;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        var enemy = target.GetComponent<Enemy>();
        ApplyDamage(turret, enemy);
        if(enemy.health <= 0)
        {
            Destroy(target.gameObject);
        }

        Destroy(gameObject);
    }

    Dictionary<string, List<string>> isEfficient = new Dictionary<string, List<string>>()
    {
        { "Cold", new List<string>{ "Hot" } },
        { "Hot", new List<string>{ "Cold" } }
    };

    private void ApplyDamage( Turret turret, Enemy enemy )
    {
        int damage = turret.baseDamage;
        var turretType = turret.type;
        var enemyType = enemy.type;
        if( isEfficient[turretType].Contains(enemyType) )
        {
            damage *= 2;
        }
        enemy.health -= damage;

        gameManager.IncreaseDamageDone(turretType, (float)damage);
    }
}