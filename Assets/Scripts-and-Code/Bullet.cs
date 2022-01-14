using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    [HideInInspector] public Transform enemy;
    [SerializeField] float speed;
    [SerializeField] GameObject particleEffect;

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
        if (collision.gameObject.transform == enemy)
        {
            if (collision.CompareTag("Enemy"))
            {
                // knock back enemy
                EnemyKnockback enemyKB = collision.GetComponent<EnemyKnockback>();
                enemyKB.knockBackTimer = enemyKB.knockBackTimerCountdown;
                enemyKB.knockBackSourcePosition = transform.position;
                GlobalAudioManager.instance.Play("Enemy Hit");
            }
            // enemy death animation gameObject
            else if (collision.CompareTag("EnemyDeath"))
            {
                collision.GetComponent<Animator>().SetBool("BulletHit", true);
                GlobalAudioManager.instance.Play("Enemy Death");
            }

            HitEffect(collision);
            Destroy(gameObject);
        }
    }

    void HitEffect(Collider2D collision)
    {   
        // hit effects and audio
        GameObject hitEffect = Instantiate(particleEffect, collision.transform.position, Quaternion.identity);
        Destroy(hitEffect, 0.5f);
    }
}
