using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerCombat playerCombat;
    [SerializeField] private Image healthBackground;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBackground.fillAmount = (float)playerCombat.playerHealth / 10;
        totalHealthBar.fillAmount = (float)playerCombat.playerHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = (float)playerCombat.currentHealth / 10;
    }
}
