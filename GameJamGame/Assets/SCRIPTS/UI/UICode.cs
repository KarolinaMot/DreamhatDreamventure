using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UICode : MonoBehaviour
{
    public PlayerCombat playerCombat;
    [SerializeField] private Image healthBackground;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image currentXp;
    public TMP_Text coinNumber;



    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBars();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerCombat.currentLifes / 10;
        healthBackground.fillAmount = playerCombat.totalLifes / 10;
        currentXp.fillAmount = playerCombat.currentXp;
        coinNumber.text = playerCombat.currentCoins.ToString();

    }

    public void OpenHub()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateHealthBars()
    {
        healthBackground.fillAmount = playerCombat.currentLifes / 10;
        totalHealthBar.fillAmount = playerCombat.currentLifes / 10;
    }
}
