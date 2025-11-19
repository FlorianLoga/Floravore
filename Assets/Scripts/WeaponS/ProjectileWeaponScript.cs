using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponScript : WeaponScript
{
    public GameObject projectilePrefab;
    public float weaponRange;
    public LayerMask whatIsEnemy;

    private float cooldownTimer;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = stats[weaponLevel].cooldown;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange, whatIsEnemy);

            if (enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Transform target = enemies[Random.Range(0, enemies.Length)].transform;
                    Vector3 direction = (target.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
     
                    proj.GetComponent<Projectile>().Init(
                        target,
                        stats[weaponLevel].damage,
                        stats[weaponLevel].speed
                    );
                }
            }
        }
    }
}
