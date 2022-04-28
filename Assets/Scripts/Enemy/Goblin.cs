using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    EnemyMovement enemyMove;

    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemyMove = GetComponent<EnemyMovement>();
        enemyMove.moveSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
