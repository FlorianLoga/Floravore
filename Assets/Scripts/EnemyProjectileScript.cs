using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public float damage = 10f;

    private Vector2 moveDirection;

    public void Init(Vector2 direction, float projectileSpeed)
    {
        moveDirection = direction.normalized;
        speed = projectileSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementScript.Instance.TakeDMG(damage);
            Destroy(gameObject);
        }
    }
}
