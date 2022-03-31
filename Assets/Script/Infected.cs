using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Infected : MonoBehaviour
{

    public static event System.Action PlayerSpotted;


    public float waitTime = 0;
    public float timeDis = 0f;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;

    public Transform Paths;
    Transform player;

    public bool isDistracted;

    public Vector3[] waypoints;

    private NavMeshAgent navMesh;
    // Start is called before the first frame update


    private void Start()
    {
        isDistracted = false;
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;


        if (Paths.childCount > 1)
        {
            waypoints = new Vector3[Paths.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = Paths.GetChild(i).position;
                //waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }
            StartCoroutine(TraversePath(waypoints));


        }
    }

    private void Update()
    {
        if (DetectPlayer())
        {
            //Debug.Log("player Detected");
            if (PlayerSpotted != null)
            {
                PlayerSpotted();
                StopAllCoroutines();
            }

        }

        if (isDistracted)
        {
            //Debug.Log("isDistracted going");
            countDistraction();
        }


    }

    void OnDrawGizmos()
    {
        if (Paths.childCount > 0)
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
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);






    }

    bool DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;

                }

            }

        }
        return false;
    }

    IEnumerator TraversePath(Vector3[] waypoints)
    {
        //transform.position = waypoints[0];

        int pathIndex = 1;
        Vector3 nextWaypoint = waypoints[pathIndex];
        transform.LookAt(nextWaypoint);
        while (true)
        {
            //transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);
            navMesh.SetDestination(nextWaypoint);

            if (navMesh.remainingDistance <= navMesh.stoppingDistance)
            {

                pathIndex = (pathIndex + 1) % waypoints.Length;
                nextWaypoint = waypoints[pathIndex];
                navMesh.SetDestination(nextWaypoint);
                //Debug.Log("Next Destination is: " + pathIndex);
                yield return new WaitForSeconds(waitTime = Random.Range(0, 7));

                //yield return StartCoroutine(faceTowards(nextWaypoint));
            }
            yield return null;

        }
    }





    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SoundWave" && !collision.gameObject.GetComponentInParent<SoundWaveCheck>().getRecentWave())
        {
            //Debug.Log("Soundwave hit!");
            StopAllCoroutines();

            //collision.gameObject.GetComponentInParent<SoundWave>().setRecentWave(true);

            // Debug.Log("this wave has been hit");
            Vector3 whereToGo = collision.transform.position;
            navMesh.SetDestination(whereToGo);
            isDistracted = true;
            collision.gameObject.GetComponentInParent<SoundWaveCheck>().setRecentWave(true);
            timeDis = 0;
        }
    }

    void countDistraction()
    {

        //Debug.Log("starting counter");
        timeDis = timeDis + 1 * Time.deltaTime;

        if (timeDis >= 6.0 && Paths.childCount > 0)
        {
            Debug.Log("going back");


            navMesh.SetDestination(waypoints[Random.Range(0, waypoints.Length)]);

            if (navMesh.remainingDistance <= navMesh.stoppingDistance)
            {
                //Debug.Log("restarting patrol");
                isDistracted = false;
                if (Paths.childCount > 0)
                    StartCoroutine(TraversePath(waypoints));

            }

        }

    }

}
