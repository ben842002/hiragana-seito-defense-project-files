using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{   
    // this is referenced in EnemyMovement.cs
    public static Transform[] waypoints;

    void Awake()
    {   
        // create an array that can store the number of waypoints in the scene 
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {   
            // assign element to a waypoint 
            waypoints[i] = transform.GetChild(i);
        }   
    }
}
