using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovementController : MonoBehaviour {
    //declares speed and rotation
    float speed;

    //rigid body to apply force
    Rigidbody rb;

    //things for turning
    bool accelerate;
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
        speed = 8f;
        accelerate = false;

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
        rotateDrag = .025f;

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
        //Debug.Log(transform.localRotation.x);
        //MOVEMENT AND MOMENTUM----------------------------------------------------------------------------------------
        //If the W key is being held down
        if (Input.GetKeyDown(KeyCode.W))
        {
            accelerate = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            accelerate = false;
        }
        
        ////applies a small boost to the player
        if (Input.GetKeyDown(KeyCode.S) && boostTimer == boostCooldown && !lockPlayer)
        {
            
            boostTimer = 0;
            rb.AddForce(transform.up * 750);
        }
        //locks player in place
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lockPlayer = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            lockPlayer = false;
        }
        if (lockPlayer)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rotateSpeed = 0;
        }


        //ROTATION AND TURNING --------------------------------------------------------------------------------------------
        //if A is held down then rotate to the right(clockwise)
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotateClockwise = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rotateClockwise = false;
        }
        

        //if D is held down then rotate to the left(counter clockwise)
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotateCounterClock = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
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
            boostTimer++;
        //move object
        if (accelerate && !lockPlayer)
        {
            //Debug.Log(Time.deltaTime);
            rb.AddForce(transform.up * speed);
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
