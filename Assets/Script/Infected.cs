using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    public Transform Paths;
    // Start is called before the first frame update


    private void Start()
    {
        Vector3[] waypoints = new Vector3[Paths.childCount];
        for (int i = 0; i <waypoints.Length; i ++)
        {
            waypoints[i] = Paths.GetChild(i).position;
        
        }
    }

    void OnDrawGizmos()
    {
        Vector3 startPosition = Paths.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in Paths)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }
}
