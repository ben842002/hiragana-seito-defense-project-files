using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMoveMainMenu : MonoBehaviour
{   
    // This script is very similar to EnemyMovement.cs except it is for the Main Menu. Any game logic/functionality is removed

    SpriteRenderer sr;
    Rigidbody2D rb;
    TMP_Text hiraganaText;

    [SerializeField] Transform startPoint;
    [SerializeField] string[] hiragana;
    int hiraganaIndex = 0;

    // waypoint movement
    [SerializeField] float moveSpeed;
    private Transform target;
    private int waypointIndex = 0;

    // for animations
    [Header("If the object is facing right, toggle isFlipped to true")]
    [SerializeField] bool isFlipped;
    bool facingRight;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        hiraganaText = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        // initial location is at starting point
        transform.position = startPoint.position;

        // first waypoint
        target = Waypoints.waypoints[0];

        // set to random hiragana word
        hiraganaIndex = Random.Range(0, hiragana.Length);
        hiraganaText.text = hiragana[hiraganaIndex];
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
        // When enemy reaches last waypoint, reset position and repeat movement cycle but change hiragana word.
        // This basically gives an illusion that a new enemy has spawned
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {   
            // reset position to starting point and repeat waypoint movement
            transform.position = startPoint.position;
            target = Waypoints.waypoints[0];
            waypointIndex = 0;

            // display next hiragana text. If the end is reached, loop back to first element
            if (hiraganaIndex + 1 >= hiragana.Length)
                hiraganaIndex = 0;
            else
                hiraganaIndex++;

            hiraganaText.text = hiragana[hiraganaIndex];

            return;
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
