using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public PlayerCombat playerCombat;

    int playerTotalLifes;
    float playerCurrentLifes;
    int playerCurrentAtk;
    float playerCurrentDef;
    int playerCurrentMoney;
    float playerCurrentXP;

    public void OnSceneChange()
    {
        playerTotalLifes = playerCombat.totalLifes;
        playerCurrentLifes = playerCombat.currentLifes;
        playerCurrentAtk = playerCombat.currentAtk;
        playerCurrentDef = playerCombat.currentDef;
        playerCurrentXP = playerCombat.currentXp;
        playerCurrentMoney = playerCombat.currentCoins;

        PlayerPrefs.SetInt("PlayerTotalLifes", playerTotalLifes);
        PlayerPrefs.SetFloat("PlayerCurrentLifes", playerCurrentLifes);
        PlayerPrefs.SetInt("PlayerCurrentAtk", playerCurrentAtk);
        PlayerPrefs.SetFloat("PlayerCurrentDef", playerCurrentDef);
        PlayerPrefs.SetInt("PlayerCurrentMoney", playerCurrentMoney);
        PlayerPrefs.SetFloat("PlayerCurrentXP", playerCurrentXP);
    }
}
