using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{   
    [Header("Bullet Prefab")]
    public GameObject bulletPrefab;

    [Header("Turret Cannon")]
    [SerializeField] Transform partToRotate;

    public void RotateTurret(Transform enemy)
    {   
        Vector2 direction = (enemy.position - transform.position).normalized;
        float rotation_Z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation_Z);
    }
}
