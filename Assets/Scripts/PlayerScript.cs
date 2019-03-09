using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    float weight;
    public float pollenAmount;
    public float altitude;

    GameObject player;
    Rigidbody rb;

    AudioSource audioData;
    public bool swooped = false;

    //UI related
    [SerializeField] Text altitudeText = null;

    void Start()
    {
        player = this.gameObject;
        rb = GetComponent<Rigidbody>();
        weight = 1.0f;
        pollenAmount = 0.0f;
        audioData = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        altitude = player.transform.position.y;
        altitudeText.text = "Altitude: " + altitude.ToString("F1");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pollen")
        {
            pollenAmount += 1.0f;  //could add properties to pollen object of different sizes
            weight += 0.02f;
            rb.mass = weight;
            Destroy(col.gameObject.transform.parent.gameObject);
        }

        if (col.gameObject.tag == "Grass")
        {
            player.GetComponent<BeeMovementController>().speed = 1.0f;
            weight -= 0.01f;
            pollenAmount -= 0.5f;
        }

        if (col.gameObject.tag == "Bird")
        {
            swoop();
        }
    }

    void OnTriggerExit(Collider col) 
    {
        if (col.gameObject.tag == "Grass")
        {
            player.GetComponent<BeeMovementController>().speed = 5.0f;
        }
    }

    void swoop()
    {
        player.transform.position = new Vector3(-108, 5, -108);
        player.pollenAmount -= player.pollenAmount/3;            
        audioData.Play(0);
    }
}
