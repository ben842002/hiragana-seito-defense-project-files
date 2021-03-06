using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MinibossMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public bool canMove;

    [SerializeField] int livesCost;

    [HideInInspector] public Waypoints wayP;
    public float moveSpeed;
    private Transform target;
    private int waypointIndex = 0;

    bool isFlipped = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        target = wayP.waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Once enemy is within reasonable distance of the waypoint, move onto to the next waypoint
        // Key Note: We didn't use Vector3.Distance because the sqr root operation is taxing (especially per frame for multiple instances of enemies)
        if ((transform.position - target.position).sqrMagnitude <= 0.2f * 0.2f)
            GetNextWaypoint();
    }

    private void FixedUpdate()
    {   
        if (canMove)
        {
            Vector3 direction = (target.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(direction.x, direction.y);
            FaceWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // when enemy reaches last checkpoint
        if (waypointIndex >= wayP.waypoints.Length - 1)
        {
            // adjust lives
            GameMaster.gm.RemoveLives(livesCost, gameObject);

            // decrement enemyCount for waveSpawner 
            WaveSpawner.UpdateEnemyCounter();

            Destroy(gameObject);

            return; // destroy is somewhat of a lengthy operation in terms of PC time, so we need to avoid having code being called after
        }

        waypointIndex++;
        target = wayP.waypoints[waypointIndex];
    }

    // this alternative to new Vector3(-1 , 1, 1) is better because there is no stuttering.
    public void FaceWaypoint()
    {
        // when the player is to the LEFT and isFlipped is true, ROTATE 180 DEGREES
        if (target.position.x < transform.position.x && isFlipped == true)
        {
            sr.flipX = true;
            isFlipped = false;
        }
        // when the player is to the RIGHT and isFlipped is false, ROTATE 180 DEGREES
        else if (target.position.x > transform.position.x && isFlipped == false)
        {
            sr.flipX = false;
            isFlipped = true;
        }
    }
}
