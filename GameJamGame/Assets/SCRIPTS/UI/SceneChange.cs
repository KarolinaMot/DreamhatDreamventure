using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public CharacterController2D characterController;

    int playerTotalLifes;
    float playerCurrentLifes;
    int playerCurrentAtk;
    float playerCurrentDef;
    int playerCurrentMoney;
    float playerCurrentXP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneChange()
    {
        playerTotalLifes = playerCombat.totalLifes;
        playerCurrentLifes = playerCombat.currentLifes;
        playerCurrentAtk = playerCombat.currentDamage;
        playerCurrentDef = playerCombat.currentDef;
        playerCurrentXP = characterController.currentXp;
        playerCurrentMoney = characterController.currentCoins;

        PlayerPrefs.SetInt("PlayerTotalLifes", playerTotalLifes);
        PlayerPrefs.SetFloat("PlayerCurrentLifes", playerCurrentLifes);
        PlayerPrefs.SetInt("PlayerCurrentAtk", playerCurrentAtk);
        PlayerPrefs.SetFloat("PlayerCurrentDef", playerCurrentDef);
        PlayerPrefs.SetInt("PlayerCurrentMoney", playerCurrentMoney);
        PlayerPrefs.SetFloat("PlayerCurrentXP", playerCurrentXP);
    }
}
