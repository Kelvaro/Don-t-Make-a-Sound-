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
    public LineRenderer linerenderer;


    public float vertexCount = 12;
    public float Point2Ypositio = 0.5f;

    public bool WaveIstantiated = false;

    public GameObject LineTesting;


    // Update is called once per frame
    void Update()
    {
      
        if (LineTesting != null)
        {
            addCollision(linerenderer);
            timer = timer + 1 * Time.deltaTime;
            Point2.transform.localPosition = new Vector3((Point1.transform.localPosition.x + Point3.transform.localPosition.x), Point2Ypositio, (Point1.transform.localPosition.z + Point3.transform.localPosition.z) / 2);
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

            //deleteCollision(linerenderer);

            linerenderer.positionCount = pointList.Count;
            linerenderer.SetPositions(pointList.ToArray());

            


            if (timer >= 1.5)
            {
                if (LineTesting != null)
                {
                    Destroy(LineTesting);
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


    }

    void deleteCollision(LineRenderer lineRenderer)
    {
        MeshCollider meshCollider = lineRenderer.gameObject.GetComponent<MeshCollider>();
        Destroy(meshCollider);
    }

    public void createWave(float xAxis, float zAxis)
    {
        WaveIstantiated = true;
        for (int i = 0; i < 8; i++)
        {
            Instantiate(LineTesting, new Vector3(xAxis, 4, zAxis), Quaternion.Euler(0, 0 + (45 * i), 90));
            
            
            //linerenderer.transform.position = position;
        }
        // Mesh mesh = new Mesh();
        //linerenderer.gameObject.AddComponent<MeshCollider>();
        //linerenderer.BakeMesh(mesh, true);
        //Instantiate(waves, position, Quaternion.Euler(0, 0, 90));
       
    }

    public void destroyWave()
    {
        Destroy(LineTesting);
    }

    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("function running");

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Soundwave collision detected from the player");

        }
        if (collision.gameObject.tag == "Terrain")
        {
            Debug.Log("Soundwave collision detected");

        }

        if (collision.gameObject.tag == "test")
        {
            Debug.Log("Soundwave collision detected");

        }

    }

    public void PullTrigger(Collider c)
    {

        if (c.gameObject.tag == "Terrain")
        {
            Debug.Log("Soundwave collided with Terrain");
        }

    }
  

}
