using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float speed;
    public float chargePower;
    bool canpickup;
    bool hasItem;

    //public GameObject playerModel;
    public GameObject whatIgrabbed;

    [SerializeField]
    private SoundWave waves;

    private void Start()
    {
        canpickup = false;
        hasItem = false;
        chargePower = 0;
    }

    void Update()
    {
        //upon key click, pick up the object and follow the player
        //upon mouse click, throw the object with a certain speed and ensure it spawns the soundwavwe upon collision with the ground, walls etc
        //this.transform.position = playerPoint.position;
        pickUp();

    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canpickup = true;
            whatIgrabbed = collision.gameObject;
            Debug.Log("OnTriggerEnter processing");
        }

        if ((collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Terrain2") && this.transform.parent==null)
        {
            float xAx = transform.position.x;
            float zAx = transform.position.z;
            waves.createWave(xAx, zAx);
            Destroy(this.gameObject);
               
        }
    }

    void OnTriggerExit(Collider other)
    {
        canpickup = false;
        whatIgrabbed = null;
    }

    void pickUp()
    {
        if (canpickup == true) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown("e"))  // can be e or any key
            {
                //this.GetComponent<Rigidbody>().isKinematic = true;
                this.GetComponent<Rigidbody>().isKinematic = true;
                //makes the object become a child of the parent so that it moves along with its parent
                this.transform.position = whatIgrabbed.transform.position; // sets the position of the object to your hand position
                this.transform.parent = whatIgrabbed.transform;
                this.transform.localPosition = new Vector3(0, 0.2f, 0.4f);
                this.transform.localRotation = Quaternion.Euler(0,0,0);
                hasItem = true;
                // Debug.Log("Item Pick up processed");
            }


        }

        if (hasItem && Input.GetMouseButton(0))
        {

            chargePower = 1.0f / 1000.0f + chargePower;
            

            Debug.Log("Charge Power level is " + chargePower);
        }
        if (hasItem && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Throwing Item");
            if (chargePower > 5)
            {
                chargePower = 5;
            }
            switch ((int)chargePower)
            {
                case 0:
                    speed = 20f;
                    waves.setSpeed(3f);
                    break;
                case 1:
                    speed = 25f;
                    waves.setSpeed(5f);
                    break;
                case 2:
                    speed = 30f;
                    waves.setSpeed(7f);
                    break;
                case 3:
                    speed = 35f;
                    waves.setSpeed(10f);
                    break;
                case 4:
                    speed = 40f;
                    waves.setSpeed(13f);
                    break;
                case 5:
                    speed = 45f;
                    waves.setSpeed(16f);
                    break;
                default:
                    speed = 10f;
                    waves.setSpeed(2f);
                    break;

            }


            this.transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        }
    }



    
}
