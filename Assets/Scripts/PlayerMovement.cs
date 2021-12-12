using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    PlayerStats stats;

    Vector2 movementInput;
    const string Horizontal = "Horizontal";
    const string Vertical = "Vertical";
    const string Movement = "Movement";

    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stats = PlayerStats.instance;
    }

    private void FixedUpdate()
    {
        Vector2 movement = movementInput.normalized * stats.moveSpeed;
        rb.velocity = new Vector2(movement.x, movement.y);


    }

    // Update is called once per frame
    void Update()
    {   
        if (PauseMenu.GameIsPaused == false)
        {
            // get movement input from player. Vector is used in FixedUpdate
            movementInput.x = Input.GetAxisRaw(Horizontal);
            movementInput.y = Input.GetAxisRaw(Vertical);

            animator.SetFloat(Movement, movementInput.sqrMagnitude);

            // flip the player.localScale when moving left or right 
            if (movementInput.x > 0 && !facingRight)
                Flip();
            else if (movementInput.x < 0 && facingRight)
                Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;                 // face opposite direction
        Vector3 theScale = transform.localScale;    // get the localScale
        theScale.x *= -1;                           // flip the X axis
        transform.localScale = theScale;            // apply adjusted scale to the localScale
    }
}
