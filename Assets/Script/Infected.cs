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
    public AudioClip alert;
    private NavMeshAgent navMesh;
    // Start is called before the first frame update


    private void Start()
    {
        isDistracted = false;
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;

        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = alert;
        if (Paths.childCount > 1)
        {
            waypoints = new Vector3[Paths.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = Paths.GetChild(i).position;
              
            }
            StartCoroutine(TraversePath(waypoints));


        }
    }

    private void Update()
    {
        if (DetectPlayer())
        {
 
            if (PlayerSpotted != null)
            {
                PlayerSpotted();
                StopAllCoroutines();
            }

        }

        if (isDistracted)
        {

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
        

        int pathIndex = 1;
        Vector3 nextWaypoint = waypoints[pathIndex];
        transform.LookAt(nextWaypoint);
        while (true)
        {
            navMesh.SetDestination(nextWaypoint);

            if (navMesh.remainingDistance <= navMesh.stoppingDistance)
            {

                pathIndex = (pathIndex + 1) % waypoints.Length;
                nextWaypoint = waypoints[pathIndex];
                navMesh.SetDestination(nextWaypoint);

                yield return new WaitForSeconds(waitTime = Random.Range(0, 7));

           
            }
            yield return null;

        }
    }





    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SoundWave")
        {
            GetComponent<AudioSource>().Play();
            Debug.Log("Soundwave hiting infected!");
            StopAllCoroutines();

           

            Vector3 whereToGo = collision.transform.position;
            navMesh.SetDestination(whereToGo);
            isDistracted = true;
            collision.gameObject.GetComponentInParent<SoundWaveCheck>().setRecentWave(true);
            timeDis = 0;
        }
    }

    void countDistraction()
    {

        timeDis = timeDis + 1 * Time.deltaTime;

        if (timeDis >= 6.0 && Paths.childCount > 0)
        {



            navMesh.SetDestination(waypoints[Random.Range(0, waypoints.Length)]);

            if (timeDis > 10)
            {
 
                navMesh.enabled = false;
                navMesh.enabled = false;
            }

            if (navMesh.remainingDistance <= navMesh.stoppingDistance)
            {

                timeDis = 0;
                isDistracted = false;
                if (Paths.childCount > 0)
                    StartCoroutine(TraversePath(waypoints));

            }
          

        }

    }

}
