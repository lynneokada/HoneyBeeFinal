using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    float weight;
    public float pollenValue;
    public float altitude;

    GameObject bee;
    Rigidbody rb;

    //UI related
    [SerializeField] Text altitudeText = null;

    void Start()
    {
        bee = this.gameObject;
        rb = GetComponent<Rigidbody>();
        weight = 1.0f;
        pollenValue = 0.0f;
    }

    void FixedUpdate()
    {
        altitude = bee.transform.position.y;
        altitudeText.text = "Altitude: " + altitude.ToString("F1");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pollen")
        {
            pollenValue += 1.0f;  //could add properties to pollen object of different sizes
            weight += 0.02f;
            rb.mass = weight;
            Destroy(col.gameObject.transform.parent.gameObject);
        }

        if (col.gameObject.tag == "Grass")
        {
            Debug.Log("slow down");
        }

        if (col.gameObject.tag == "Bird")
        {
            Debug.Log("BIRB");
        }
    }
}
