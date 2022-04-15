using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    // Start is called before the first frame update
    //public SoundWave[] childTesting;
    public float speed = 5f;



    public Transform Point1;
    public Transform Point2;
    public Transform Point3;

    public float timer = 0f;
    public float timerStopAt = 2f;

    public LineRenderer linerenderer;


    public float vertexCount = 12;
    public float Point2Ypositio = 0.5f;

    public bool WaveIstantiated = false;
    private bool WaveCollided = false;

    public GameObject OriginPoint;

    public GameObject SoundObject;
    public GameObject Waves;

    // Update is called once per frame

    private void Start()
    {
        WaveCollided = false;
    }

    void Update()
    {

        if (OriginPoint != null)
        {
            addCollision(linerenderer);
            timer = timer + 1 * Time.deltaTime;
            Point2.transform.localPosition = new Vector3(((Point1.transform.localPosition.x + Point3.transform.localPosition.x)/2), Point2Ypositio, (Point1.transform.localPosition.z + Point3.transform.localPosition.z) / 2);
            var pointList = new List<Vector3>();

            for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
            {
                var tangent1 = Vector3.Lerp(Point1.localPosition, Point2.localPosition, ratio);
                var tangent2 = Vector3.Lerp(Point2.localPosition, Point3.localPosition, ratio);
                var curve = Vector3.Lerp(tangent1, tangent2, ratio);

                pointList.Add(curve);
            }
            Point1.transform.Translate(-speed * Time.deltaTime, 0, (-speed / 3) * Time.deltaTime);
            Point3.transform.Translate(-speed * Time.deltaTime, 0, (speed / 3) * Time.deltaTime);
            Point2Ypositio = Point2Ypositio + speed * Time.deltaTime;



            linerenderer.positionCount = pointList.Count;
            linerenderer.SetPositions(pointList.ToArray());




            if (timer >= timerStopAt)
            {
                if (OriginPoint != null)
                {
                    //Debug.Log("Soundwave destroyed");
                    Destroy(OriginPoint);
                    timer = 0;
                    WaveIstantiated = false;
                }

            }
        }


        //make the soundwave move to a specific direction
        //have 8 array of soundwave "parts"
        //for each iteration inside the array, have it increment the rotation by 1/8 and then move in that specific direction


    }

    void addCollision(LineRenderer lineRenderer)
    {
        MeshCollider meshCollider = lineRenderer.gameObject.GetComponent<MeshCollider>();


        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, false);
        meshCollider.sharedMesh = mesh;

        //this method would continuously update the soundwave's collision as it expands further away.
    }


    public void createWave(float xAxis, float zAxis)
    {
        WaveIstantiated = true;
        OriginPoint = Instantiate(SoundObject, new Vector3(xAxis, 4, zAxis), Quaternion.Euler(0, 0, 0));
        //creates 8 waves each rotating in 45 degrees from the starting point

        for (int i = 0; i < 8; i++)
        {
            GameObject newWaves = Instantiate(Waves, new Vector3(xAxis, 2, zAxis), Quaternion.Euler(0, 0 + (45 * i), 90));
            newWaves.transform.parent = OriginPoint.transform;

            //creates 8 waves each rotating in 45 degrees from the starting point
            //8 waves since there are 8 directions in the compass.
        }
    }


    public void PullTrigger(Collider c)
    {
        //method that takes collider as a parameter.
        //required since the wave is a child.
        if (c.gameObject.tag == "Terrain")
        {
            //Debug.Log("Soundwave collided with Terrain");
            Destroy(Waves);
        }

    }

    public bool getRecentWave()
    {
        return WaveCollided;
    }

    public void setRecentWave(bool hit)
    {
        WaveCollided = hit;
    }

    public void setSpeed(float WaveSpeed)
    {
        speed = WaveSpeed;
    }

}
