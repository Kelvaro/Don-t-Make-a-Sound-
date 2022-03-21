using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    public float speed = 15f;
    public float waitTime = .5f;
    public Transform Paths;
    // Start is called before the first frame update


    private void Start()
    {
        Vector3[] waypoints = new Vector3[Paths.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = Paths.GetChild(i).position;

        }
        StartCoroutine(TraversePath(waypoints));


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

    IEnumerator TraversePath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int pathIndex = 1;
        Vector3 nextWaypoint = waypoints[pathIndex];

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);
            if (transform.position == nextWaypoint)
            {
                pathIndex = (pathIndex + 1) % waypoints.Length;
                nextWaypoint = waypoints[pathIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
            
        }
    }

}
