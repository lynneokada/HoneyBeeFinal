using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovementController : MonoBehaviour {
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

    //controls
    KeyCode forward;
    KeyCode backward;
    KeyCode leftRotate;
    KeyCode rightRotate;
    KeyCode up;
    KeyCode down;
    KeyCode boost;

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

        if (gameObject.tag == "Player1")
        {
            forward = KeyCode.W;
            backward = KeyCode.S;
            leftRotate = KeyCode.A;
            rightRotate = KeyCode.D;
            up = KeyCode.Z;
            down = KeyCode.C;
            boost = KeyCode.X;
        } else if (gameObject.tag == "Player2") {
            forward = KeyCode.P;
            backward = KeyCode.Semicolon;
            leftRotate = KeyCode.L;
            rightRotate = KeyCode.Quote;
            up = KeyCode.Comma;
            down = KeyCode.Slash;
            boost = KeyCode.Period;
        }
	}
	
	void Update () {


        if (transform.localRotation.eulerAngles.x > 1f || transform.localRotation.eulerAngles.x < -1f || transform.localRotation.eulerAngles.z > 91f || transform.localRotation.eulerAngles.z < 89f)
        {
            transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 90);
        }

        if (Input.GetKey(forward))
        {
            rb.AddForce(transform.up * speed);
        }
        if (Input.GetKey(backward))
        {
            rb.AddForce(-transform.up * bSpeed);
        }
        
        if (Input.GetKey(down)) 
        {
            rb.AddForce(0,verticalForceValue,0);
        }
        if (Input.GetKey(up))
        {
            rb.AddForce(0,-verticalForceValue,0);
        }

        ///nitro but also attack
        if (Input.GetKeyDown(boost) && boostTimer == boostCooldown && !lockPlayer)
        {
            boostTimer = 0;
            GetComponent<PlayerAudioScript>().PlayBoostSound();
            rb.AddForce(transform.up * 250);
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

        //clockwise rotation
        if (Input.GetKeyDown(rightRotate))
        {
            rotateClockwise = true;
        }
        if (Input.GetKeyUp(rightRotate))
        {
            rotateClockwise = false;
        }
        
        //counter clockwise rotation
        if (Input.GetKeyDown(leftRotate))
        {
            rotateCounterClock = true;
        }
        if (Input.GetKeyUp(leftRotate))
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
