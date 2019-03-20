using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenRingScript : MonoBehaviour
{
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player1" && gameObject.tag == "Player1Ring")
        {
            col.gameObject.GetComponent<PlayerScript>().pollenAmount += 1.0f;
            foreach(Transform child in transform)
            {
                child.gameObject.GetComponent<PollenScript>().player = col.gameObject;
                child.gameObject.GetComponent<PollenScript>().didExit = true;
            }
        }

        if (col.gameObject.tag == "Player2" && gameObject.tag == "Player2Ring")
        {
            col.gameObject.GetComponent<PlayerScript>().pollenAmount += 1.0f;
            foreach(Transform child in transform)
            {
                child.gameObject.GetComponent<PollenScript>().player = col.gameObject;
                child.gameObject.GetComponent<PollenScript>().didExit = true;
            }
        }
    }
}
