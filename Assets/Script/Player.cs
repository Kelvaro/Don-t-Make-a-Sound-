using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float playerSpeed = 10;
    private float runningSpeed = 20;
    private float delayTime = 0.3f;
    private float timer = 0.3f;
    public float playerAngle;
    [SerializeField]
    private SoundWave waves;
    public float speed = 3f;
    public Vector3 playerFace;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        turnPlayer();
        movePlayer();
      
    }

    void turnPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point); // Look at the point
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); // Clamp the x and z rotation
        }
    }

    void movePlayer()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift) && ((verticalMovement > 0 || verticalMovement <0) || (horizontalMovement <0||horizontalMovement >0)) )
        {
            //optimize the following later:
            //-time between spawning soundwaves.
            float xAx = transform.position.x;
            float zAx = transform.position.z;
            timer += Time.deltaTime;
            if (timer > delayTime)
            {

                waves.createWave(xAx, zAx);
                timer = timer - delayTime;
            }

            GetComponent<Rigidbody>().velocity = new Vector3
        (horizontalMovement * runningSpeed,
        0,
        verticalMovement * runningSpeed

        );


        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3
              (horizontalMovement * playerSpeed,
              0,
              verticalMovement * playerSpeed

              );
            timer = 0.3f;
        }

    }



}
