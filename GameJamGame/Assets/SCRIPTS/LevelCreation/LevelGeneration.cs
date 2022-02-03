using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms; //index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 -->LRBT
    public GameObject pathRooms;
    public GameObject sceneObjects;
    Transform playerSpawn;
    bool playerSpawned = false;

    private int direction;
    public float moveAmountX;
    public float moveAmountY;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    public LayerMask room;

    private int downCounter =0;
    GameObject newRoom;
    public bool stopLevelGeneration = false;
    // Start is called before the first frame update
    void Awake()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        playerSpawn = startingPositions[randStartingPos];
        newRoom = Instantiate(rooms[0], transform.position, Quaternion.identity);
        newRoom.transform.parent = pathRooms.transform;

        direction = Random.Range(1, 7);
    }

    private void Update()
    {
        if(timeBtwRoom <= 0 && !stopLevelGeneration)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

        if (stopLevelGeneration && !playerSpawned)
        {
            Instantiate(sceneObjects, playerSpawn);
            playerSpawned = true;
        }
    }
    private void Move()
    {
        if(direction ==1 || direction == 2){ //moves left
            if(transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, 4);
                newRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                newRoom.transform.parent = pathRooms.transform;

                direction = Random.Range(1, 7);
                if(direction == 3) { direction = 2;}
                if(direction == 4) { direction = 5;}
            }
            else { direction = 5; }
            
        }
        else if(direction ==3 || direction == 4){ //moves right
            if(transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, 4);
                newRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                newRoom.transform.parent = pathRooms.transform;
                direction = Random.Range(3, 7);
            }
            else { direction = 5; }
        } 
        else if(direction == 5 || direction == 6){ //moves down
            downCounter++;
            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                if (roomDetection.GetComponent<RoomType>().type != 3 && roomDetection.GetComponent<RoomType>().type != 1)
                {   
                    if(downCounter >=2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        newRoom = Instantiate(rooms[3], transform.position, Quaternion.identity);
                        newRoom.transform.parent = pathRooms.transform;
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        newRoom = Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        newRoom.transform.parent = pathRooms.transform;
                    }                    
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountY);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                newRoom = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                newRoom.transform.parent = pathRooms.transform;

                direction = Random.Range(1, 5);
            }
            else
            {
                stopLevelGeneration = true;
            }
            
        }
        Debug.Log(direction);
    }
}
