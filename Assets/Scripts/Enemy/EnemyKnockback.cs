using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    // Attach this script to enemy that you want to have a knockback

    [HideInInspector] public float knockBackTimer;
    [HideInInspector] public Vector3 knockBackSourcePosition;

    [Header("Adjustable Values")]
    public float knockBackTimerCountdown = 0.1f;
    public float knockBackForce = 12.5f;

    /// <summary>
    /// Knockbacks the enemy based on turret's Vector3 position at start time of code.
    /// </summary>
    public void Knockback(GameObject enemyGameObject)
    {
        knockBackTimer -= Time.fixedDeltaTime;

        Vector2 dir = (enemyGameObject.transform.position - knockBackSourcePosition).normalized * knockBackForce;
        enemyGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y);
    }
}
