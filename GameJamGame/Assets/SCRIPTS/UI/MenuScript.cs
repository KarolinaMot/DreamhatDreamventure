using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int playerTotalLifes = 3;
    public int playerCurrentLifes = 3;
    public int playerCurrentAtk = 1;
    public float playerCurrentDef = 0;
    public int playerCurrentMoney = 0;
    public float playerCurrentXP = 0;
    public int scene = 0;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("PlayerTotalLifes", playerTotalLifes);
        PlayerPrefs.SetFloat("PlayerCurrentLifes", playerCurrentLifes);
        PlayerPrefs.SetInt("PlayerCurrentAtk", playerCurrentAtk);
        PlayerPrefs.SetFloat("PlayerCurrentDef", playerCurrentDef);
        PlayerPrefs.SetInt("PlayerCurrentMoney", playerCurrentMoney);
        PlayerPrefs.SetFloat("PlayerCurrentXP", playerCurrentXP);
    }


    public void OpenHub()
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
