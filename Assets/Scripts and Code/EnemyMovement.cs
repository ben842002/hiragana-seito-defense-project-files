using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    // waypoint movement
    [SerializeField] float moveSpeed;
    private Transform target;
    private int waypointIndex = 0;

    [SerializeField] int livesCost = 1;

    // for animations
    [Header("If the object is facing right, toggle isFlipped to true")]
    [SerializeField] bool isFlipped;
    bool facingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        // first waypoint
        target = Waypoints.waypoints[0];
    }

    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(direction.x, direction.y);

        FaceWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Once enemy is within reasonable distance of the waypoint, move onto to the next waypoint
        // Key Note: We didn't use Vector3.Distance because the sqr root operation is taxing (especially per frame for multiple instances of enemies)
        if ((transform.position - target.position).sqrMagnitude <= 0.2f * 0.2f)       
            GetNextWaypoint(); 
    }

    void GetNextWaypoint()
    {   
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            GameMaster.gm.RemoveLives(livesCost, gameObject);
            Destroy(gameObject);

            return; // destroy is somewhat of a lengthy operation in terms of PC time, so we need to avoid having code being called after
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    /// <summary>
    /// this alternative to new Vector3(-1 , 1, 1) is better because there is no stuttering.
    /// </summary>
    public void FaceWaypoint()
    {
        // when the player is to the LEFT and isFlipped is true, ROTATE 180 DEGREES
        if (target.position.x < transform.position.x && isFlipped == true)
        {
            sr.flipX = true;
            isFlipped = false;
            facingRight = false;
        }
        // when the player is to the RIGHT and isFlipped is false, ROTATE 180 DEGREES
        else if (target.position.x > transform.position.x && isFlipped == false)
        {
            sr.flipX = false;
            isFlipped = true;
            facingRight = true;
        }
    }
}
