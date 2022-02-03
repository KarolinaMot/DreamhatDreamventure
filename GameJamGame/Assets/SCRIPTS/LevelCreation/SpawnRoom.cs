using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    public GameObject fillRooms;
    GameObject newRoom;


    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomDetection == null && levelGen.stopLevelGeneration == true)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            newRoom = Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            newRoom.transform.parent = fillRooms.transform;
            Destroy(gameObject);
        }
    }
}
