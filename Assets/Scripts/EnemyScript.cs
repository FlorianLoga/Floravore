using UnityEngine;

public class Enemy : MonoBehaviour, IDamageableEnemy
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    private Vector3 direction;
    [SerializeField]
    private GameObject destroyEffect;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float health;
    [SerializeField]
    private int expToGive;
    [SerializeField]
    private float pushTime;
    private float pushCounter;

    void FixedUpdate()
    {
        if (PlayerMovementScript.Instance.gameObject.activeSelf == true)
        {
            //urmarirea player-ului stanga dreapta
            if (PlayerMovementScript.Instance.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            //push 
            if (pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if(moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed;
                }
                if(pushCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                }
            }
            //urmarirea player-ului
            direction = (PlayerMovementScript.Instance.transform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
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
        DamageNumberControllerScript.Instance.CreateNumber(damage,transform.position);
        health -= damage;
        pushCounter = pushTime;
        if(health < 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerMovementScript.Instance.GetExp(expToGive);
            AudioControllerScript.Instance.PlayModifiedSound(AudioControllerScript.Instance.enemySlain);
        }

    }
}
