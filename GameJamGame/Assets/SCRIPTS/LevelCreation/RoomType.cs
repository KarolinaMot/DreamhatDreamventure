using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public GameObject spawnPlace;

    public void Awake()
    {
        spawnPlace = transform.GetChild(1).gameObject;
    }
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
