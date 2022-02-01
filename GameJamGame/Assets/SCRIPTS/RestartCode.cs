using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCode : MonoBehaviour
{
    public PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Awake()
    {
        playerCombat.currentLifes = playerCombat.totalLifes;
        playerCombat.currentXp = 0;
    }
}
