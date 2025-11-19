using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float damage;

    public void Init(Transform targetEnemy, float dmg, float spd)
    {
        target = targetEnemy;
        damage = dmg;
        speed = spd;

        AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.projectileWeaponSpawn);
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); 
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += transform.up * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDMG(damage);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageableEnemy enemy = collision.GetComponent<IDamageableEnemy>();
            if (enemy != null)
            {
                enemy.TakeDMG(damage);
            }

            Destroy(gameObject);
        }
    }

}
