using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float pollenAmount;
    public float altitude;

    // checkpoint management
    Vector3 checkpointLocation = new Vector3(-108, 3, -108);
    bool c1 = false;
    bool c2 = false;
    bool c3 = false;

    GameObject player;
    Rigidbody rb;
    float verticalForceValue = 10f;

    AudioSource audioData;
    public bool swooped = false;
    float rotationSpeed = 15f;
    float spinTime = 100;
    float spinTimer = 100;

    //UI related
    [SerializeField] Text altitudeText = null;

    void Start()
    {
        player = this.gameObject;
        rb = GetComponent<Rigidbody>();
        pollenAmount = 0.0f;
        audioData = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // display player's altitude
        altitude = player.transform.position.y;
        altitudeText.text = "Altitude: " + altitude.ToString("F1");

        if (spinTimer < spinTime)
        {
            player.GetComponent<Collider>().isTrigger = true;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.transform.Rotate(rotationSpeed, 0, 0, Space.Self);
            spinTimer++;
        } else {
            player.GetComponent<Collider>().isTrigger = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pollen")
        {
            if (col.gameObject.GetComponent<PollenScript>().didExit == true)
            {
                GetComponent<PlayerAudioScript>().PollenSound();
                pollenAmount += 1.0f;  //could add properties to pollen object of different sizes
                Destroy(col.gameObject.transform.parent.gameObject);
            }
        }

        if (col.gameObject.tag == "Grass")
        {
            player.GetComponent<BeeMovementController>().speed = 1.0f;
            GetComponent<PlayerAudioScript>().GrassHitSound();
            pollenAmount -= 0.5f;
        }

        if (col.gameObject.tag == "Bird")
        {
            swoop();
        }

        if (col.gameObject.tag == "Checkpoint1")
        {
            checkpointLocation = new Vector3(120.7f, 3f, -129f);
            c1 = true;
        }
        if (col.gameObject.tag == "Checkpoint2")
        {
            checkpointLocation = new Vector3(133.2f, 3f, 53.1f);
            c2 = true;
        }
        if (col.gameObject.tag == "Checkpoint3")
        {
            checkpointLocation = new Vector3(-72.4f, 3f, 108.7f);
            c3 = true;
        }
        if (col.gameObject.tag == "FinishLine")
        {
            if (c1 == true && c2 == true && c3 == true)
            {
                Debug.Log("completed race");
            }
        }
    }

    void OnTriggerExit(Collider col) 
    {
        if (col.gameObject.tag == "Grass")
        {
            player.GetComponent<BeeMovementController>().speed = 5.0f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        pollenAmount -= 1.0f;
        rb.freezeRotation = true;

        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2")
        {
            GetComponent<PlayerAudioScript>().PlayerHitSound();
            if (col.gameObject.GetComponent<PlayerScript>().rb.velocity.magnitude > rb.velocity.magnitude)
            {
                spinTimer = 0;
            } else {
                col.gameObject.GetComponent<PlayerScript>().spinTimer = 0; 
            }
        }

        if (col.gameObject.tag == "Stone" || col.gameObject.tag == "Stick" || col.gameObject.tag == "Tree")
        {
            GetComponent<PlayerAudioScript>().PlayHardCollisionSound();
        }
    }

    void swoop()
    {
        player.transform.position = checkpointLocation;        
        GetComponent<PlayerAudioScript>().PlayQuickFartSound();
    }
}
