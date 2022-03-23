using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    //float throwForce = 600;
    Vector3 objectPos;
    float distance;
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

        if (collision.gameObject.tag == "Terrain")
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

            chargePower = 1.0f / 4.0f + chargePower;
           
            

            Debug.Log("Charge Power level is " + chargePower);
        }
        if (hasItem && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Throwing Item");
            this.transform.parent = null;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 30f;

        }
    }



    
}
