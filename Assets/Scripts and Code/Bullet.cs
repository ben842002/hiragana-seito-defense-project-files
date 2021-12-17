using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    WordManager wm;

    public Transform enemy;
    [SerializeField] float speed; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wm = GameMaster.gm.GetComponent<WordManager>();
    }

    private void FixedUpdate()
    {
        if (enemy == null)
        {
            Debug.Log("Destroyed");
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
            // hit effects

            if (collision.GetComponent<EnemyDead>().isDead == true)
                collision.GetComponent<Animator>().SetBool("isDead", true);

            Destroy(gameObject);
        }
    }
}
