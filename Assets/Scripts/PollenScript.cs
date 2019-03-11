using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenScript : MonoBehaviour
{

    GameObject pollen;

    // Start is called before the first frame update
    void Start()
    {
        pollen = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2")
        {
            pollen.transform.position = Vector3.MoveTowards(pollen.transform.position, col.gameObject.transform.position, 0.1f);
        }
    }
}
