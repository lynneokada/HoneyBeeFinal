using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee2MovementController : MonoBehaviour {
    //declares speed and rotation
    public float speed = 5.0f;
    float bSpeed = 2.0f;

    //rigid body to apply force
    Rigidbody rb;

    //vertical movement
    float verticalForceValue;

    //properties for rotation
    bool rotateClockwise;
    bool rotateCounterClock;
    float rotateSpeed;
    float maxRotate;
    float rotateRatio;
    float rotateDrag;

    //special abilites
    int boostCooldown;
    int boostTimer;
    bool lockPlayer;

	// Use this for initialization
	void Start () {        
        verticalForceValue = 5;

        //initializes rigid body
        rb = GetComponent<Rigidbody>();
        rb.drag = .5f;
        rb.centerOfMass = new Vector3(0, 0, 0);
        rb.inertiaTensor = new Vector3(1, 1, 1);

        //rotation
        rotateClockwise = false;
        rotateCounterClock = false;
        rotateSpeed = 0f;
        maxRotate = 1f;
        rotateRatio = .15f;
        rotateDrag = .05f;

        //special abilites
        boostCooldown = 300;
        boostTimer = 300;
        lockPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if the player become axis misaligned rotate them back
        //if the player become axis misaligned rotate them back
        if (transform.localRotation.eulerAngles.x > 1f || transform.localRotation.eulerAngles.x < -1f || transform.localRotation.eulerAngles.z > 91f || transform.localRotation.eulerAngles.z < 89f)
        {
            //Debug.Log("Axis misaligned");
            transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 90);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.up * bSpeed);
        }
        
        if (Input.GetKey(KeyCode.B)) 
        {
            rb.AddForce(0,verticalForceValue,0);
        }
        if (Input.GetKey(KeyCode.M))
        {
            rb.AddForce(0,-verticalForceValue,0);
        }

        ////applies a small boost to the player
        if (Input.GetKeyDown(KeyCode.N) && boostTimer == boostCooldown && !lockPlayer)
        {
            boostTimer = 0;
            rb.AddForce(transform.up * 750);
        }
        // //locks player in place
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     lockPlayer = true;
        // }
        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     lockPlayer = false;
        // }
        // if (lockPlayer)
        // {
        //     rb.velocity = Vector3.zero;
        //     rb.angularVelocity = Vector3.zero;
        //     rotateSpeed = 0;
        // }


        //ROTATION AND TURNING --------------------------------------------------------------------------------------------
        //if A is held down then rotate to the right(clockwise)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateClockwise = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rotateClockwise = false;
        }
        

        //if D is held down then rotate to the left(counter clockwise)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateCounterClock = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rotateCounterClock = false;
        }
        

        //slowly decelerate rotation over time
        if (rotateSpeed < 0)
            rotateSpeed += rotateDrag;
        if (rotateSpeed > 0)
            rotateSpeed -= rotateDrag;
	}

    //Used for timers and things that need to be constant (independent of framerate)
    private void FixedUpdate()
    {
        if (boostTimer < boostCooldown)
        {
            boostTimer++;
        }

        if (rotateClockwise && rotateSpeed < maxRotate && !lockPlayer)
        {
            rotateSpeed += rotateRatio;
        }
        transform.Rotate(rotateSpeed, 0, 0, Space.Self);
        if (rotateCounterClock && rotateSpeed > -1 * maxRotate && !lockPlayer)
        {
            rotateSpeed -= rotateRatio;
        }
        transform.Rotate(rotateSpeed, 0, 0, Space.Self);
    }
}
