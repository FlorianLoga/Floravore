using UnityEngine;

public class WaterPuddle : MonoBehaviour
{
    public float damage = 5f;
    public float duration = 3f;
    public float tickRate = 0.5f;

    private void Start()
    {
        Destroy(gameObject, duration);
        InvokeRepeating(nameof(DealDamage), 0f, tickRate);
    }

    private void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Enemies"));
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<IDamageableEnemy>()?.TakeDMG(damage);
        }
    }
}
