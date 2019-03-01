using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float weight;
    public float pollenValue;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weight = 1.0f;
        pollenValue = 0.0f;
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
    }
}
