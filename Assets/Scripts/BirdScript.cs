using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    [SerializeField] private GameObject player;
    float speed;
    Vector3[] positionArray = new [] { new Vector3(400f,50f,0f), new Vector3(-400f,50f,1f), new Vector3(0f,50f,400f), new Vector3(0f,50f,-400f) }; //east, west, north, south
    Direction direction;
    float triggerHeight = 9.0f;
    bool hawkSoundPlayed = false;

    enum Direction {
        north,
        south,
        east,
        west,
        none
    };

    // Start is called before the first frame update
    void Start()
    {
        direction = Direction.none;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= triggerHeight)
        {
            speed = 200;
            if (!hawkSoundPlayed)
            {
                player.GetComponent<PlayerAudioScript>().PlayHawkScreechSound();
                hawkSoundPlayed = true;
            }
            
            if (direction == Direction.none)
            {
                if (transform.position == positionArray[0])
                {
                    direction = Direction.east;
                } else if (transform.position == positionArray[1]) {
                    direction = Direction.west;
                } else if (transform.position == positionArray[2]) {
                    direction = Direction.north;
                } else if (transform.position == positionArray[3]) {
                    direction = Direction.south;
                }
            }
            
            // Move our position a step closer to the target.
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        } else {
            speed = 150;
            hawkSoundPlayed = false;
            float step =  speed * Time.deltaTime; // calculate distance to move
            
            switch(direction)
            {
                case Direction.north:
                    transform.position = Vector3.MoveTowards(transform.position, positionArray[3], step);
                    break;
                case Direction.south:
                    transform.position = Vector3.MoveTowards(transform.position, positionArray[2], step);                    
                    break;
                case Direction.east:
                    transform.position = Vector3.MoveTowards(transform.position, positionArray[1], step);
                    break;
                case Direction.west:
                    transform.position = Vector3.MoveTowards(transform.position, positionArray[0], step);  
                    break;
                case Direction.none:
                    break;
                default:
                    break;
            }
        }

        if (transform.position == positionArray[0])
        {
            direction = Direction.none;
            gameObject.transform.eulerAngles = new Vector3(0,-90,0);
        } else if (transform.position == positionArray[1]) {
            direction = Direction.none;
            gameObject.transform.eulerAngles = new Vector3(0,90,0);
        } else if (transform.position == positionArray[2]) {
            direction = Direction.none;
            gameObject.transform.eulerAngles = new Vector3(0,0,0);
        } else if (transform.position == positionArray[3]) {
            direction = Direction.none;
            gameObject.transform.eulerAngles = new Vector3(0,180,0);
        }
    }
}
