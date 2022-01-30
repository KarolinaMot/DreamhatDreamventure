using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayer : MonoBehaviour
{
    public BossArmCode armCode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "BadPlayer" && !armCode.collisionWithPlayer)
        {
            armCode.movingTime = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "BadPlayer" && !armCode.collisionWithPlayer)
        {
            armCode.movingTime = true;
        }
    }
}
