using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public Transform enemy;
    [SerializeField] float speed; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // shoot at current word's enemy gameObject
        Vector2 moveDirection = (enemy.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // trigger death animation once word has been typed
            if (collision.GetComponent<EnemyDead>().isDead == true)
                collision.GetComponent<Animator>().SetBool("isDead", true);
            else
            {
                // knock back enemy
                EnemyKnockback enemyKB = collision.GetComponent<EnemyKnockback>();
                enemyKB.knockBackTimer = enemyKB.knockBackTimerCountdown;
                enemyKB.knockBackSourcePosition = transform.position;
            }

            // hit effects and audio

            Destroy(gameObject);
        }
    }
}
