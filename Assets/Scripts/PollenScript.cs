using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenScript : MonoBehaviour
{

    GameObject pollen;
    public bool didExit = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pollen = this.gameObject;
    }

    void Update()
    {
        if (didExit == true)
        {
            pollen.transform.position = Vector3.MoveTowards(pollen.transform.position, player.transform.position, 0.5f);
        }
    }
}
