using UnityEngine;

public class EnemySummonerScript : MonoBehaviour, IDamageableEnemy
{
    public GameObject minionPrefab;
    public Transform[] summonPoints;
    public float summonCooldown = 5f;
    public int maxMinions = 3;
    public float health = 100f;
    public GameObject destroyEffect;
    public int expToGive;
    private Rigidbody2D rb;
    private Transform player;
    private float cooldownTimer;
    private int minionCount;
    [SerializeField] private float pushTime = 0.2f;
    private float pushCounter = 0f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerMovementScript.Instance.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if (pushCounter > 0f)
        {
            pushCounter -= Time.deltaTime;
            if (moveSpeed > 0f)
            {
                moveSpeed = -moveSpeed; 
            }
            if (pushCounter <= 0f)
            {
                moveSpeed = Mathf.Abs(moveSpeed); 
            }
        }

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && minionCount < maxMinions)
        {
            SummonMinion();
            cooldownTimer = summonCooldown;
        }
    }

    void SummonMinion()
    {
        if (summonPoints.Length == 0) return;

        Transform spawnPoint = summonPoints[Random.Range(0, summonPoints.Length)];
        Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
        minionCount++;
    }

    public void TakeDMG(float damage)
    {
        DamageNumberControllerScript.Instance.CreateNumber(damage, transform.position);
        health -= damage;
        pushCounter = pushTime;

        if (health <= 0f)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerMovementScript.Instance.GetExp(expToGive);
            AudioControllerScript.Instance.PlayModifiedSound(AudioControllerScript.Instance.enemySlain);
            Destroy(gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementScript.Instance.TakeDMG(damage);
        }
    }
}
