using UnityEngine;

public class EnemyRangedScript : MonoBehaviour, IDamageableEnemy
{
    public float speed = 1f;
    public float stopDistance = 5f;
    public float attackCooldown = 2f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 1f; 


    private Transform player;
    private float cooldownTimer;
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float health;
    [SerializeField]
    private float pushTime;
    private float pushCounter;
    [SerializeField]
    private GameObject destroyEffect;
    [SerializeField]
    private int expToGive;
    [SerializeField]
    private float damage;

    void Start()
    {
        player = PlayerMovementScript.Instance.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        //urmarirea player-ului stanga dreapta
        if (PlayerMovementScript.Instance.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // Se apropie de player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            // Se opreste si ataca
            rb.linearVelocity = Vector2.zero;

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                ShootProjectile();
                cooldownTimer = attackCooldown;
            }
        }
    }

    void ShootProjectile()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<EnemyProjectile>().Init(direction,projectileSpeed);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementScript.Instance.TakeDMG(damage);
        }
    }
    public void TakeDMG(float damage)
    {
        DamageNumberControllerScript.Instance.CreateNumber(damage, transform.position);
        health -= damage;
        pushCounter = pushTime;
        if (health < 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerMovementScript.Instance.GetExp(expToGive);
            AudioControllerScript.Instance.PlayModifiedSound(AudioControllerScript.Instance.enemySlain);
        }

    }
}
