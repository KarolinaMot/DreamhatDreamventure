using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICode : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public CharacterController2D characterController2D;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Image healthBackground;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image currentXp;

    [SerializeField] private Text currentAtkText;
    [SerializeField] private Text currentHpText;
    [SerializeField] private Text currentDefText;
    public TMP_Text coinNumber;

    [SerializeField] private Text atkPrice;
    [SerializeField] private Text hpPrice;
    [SerializeField] private Text defPrice;
    [SerializeField] private Text fullPrice;

    [SerializeField] private Button hpUp;
    [SerializeField] private Button hpDown;
    [SerializeField] private Button atkUp;
    [SerializeField] private Button atkDown;
    [SerializeField] private Button defUp;
    [SerializeField] private Button defDown;
    [SerializeField] private Button purchase;

    private int shownHp;
    private float shownDef;
    private int shownAtk;
    private int maxAtk = 3;
    private float maxDef = 0.4f;
    private int maxHp = 6;

    private int shownHpPrice = 0;
    private int shownAtkPrice = 0;
    private int shownDefPrice = 0;
    private int shownPrice = 0;

    private int hpPricenum = 150;
    private int defPricenum = 100;
    private int atkPricenum = 50;

    // Start is called before the first frame update
    void Start()
    {
        shownHp = playerCombat.playerHealth;
        shownDef = playerCombat.playerDefense;
        shownAtk = playerCombat.attackDamage;
        UpdateHealthBars();
    }

    // Update is called once per frame
    void Update()
    {
        DisableButtons();

        currentHealthBar.fillAmount = (float)playerCombat.currentHealth / 10;
        currentXp.fillAmount = (float)characterController2D.xpNum;
        coinNumber.text = characterController2D.moneyNum.ToString();

        currentHpText.text = shownHp.ToString();
        currentAtkText.text = shownAtk.ToString();
        currentDefText.text = (shownDef*10f).ToString();

        atkPrice.text = shownAtkPrice.ToString();
        defPrice.text = shownDefPrice.ToString();
        hpPrice.text = shownHpPrice.ToString();
        shownPrice = (shownAtkPrice + shownDefPrice + shownHpPrice);
        fullPrice.text = shownPrice.ToString();

        if(shownPrice > characterController2D.moneyNum)
            purchase.interactable = false;
        else
            purchase.interactable = true;
    }

    void UpdateHealthBars()
    {
        healthBackground.fillAmount = (float)playerCombat.playerHealth / 10;
        totalHealthBar.fillAmount = (float)playerCombat.playerHealth / 10;
    }
    void DisableButtons()
    {
        if(shownHp <= playerCombat.playerHealth)
        {
            hpDown.interactable = false;
        }
        else
        {
            hpDown.interactable = true;
        }

        if (shownHp >= maxHp) {
            hpUp.interactable = false;
        }
        else
        {
            hpUp.interactable = true;
        }

        if (shownAtk <= playerCombat.attackDamage)
        {
            atkDown.interactable = false;
        }
        else
        {
            atkDown.interactable = true;
        }

        if(shownAtk >= maxAtk)
        {
            atkUp.interactable = false;
        }
        else
        {
            atkUp.interactable = true;
        }


        if (shownDef <= playerCombat.playerDefense)
        {
            defDown.interactable = false;
        }
        else
        {
            defDown.interactable = true;
        }

        if(shownDef >= maxDef)
        {
            defUp.interactable = false;
        }
        else
        {
            defUp.interactable = true;
        }
    }
    public void HealthPressUp()
    {
        shownHp++;
        shownHpPrice += hpPricenum;
    }
    public void AtkPressUp()
    {
        shownAtk++;
        shownAtkPrice += atkPricenum;
    }
    public void DefPressUp()
    {
        shownDef+=0.1f;
        shownDefPrice += defPricenum;
    }
    public void HealthPressDown()
    {
        shownHp--;
        shownHpPrice -= defPricenum;
    }
    public void AtkPressDown()
    {
        shownAtk--;
        shownAtkPrice -= atkPricenum;
    }
    public void DefPressDown()
    {
        if (shownDef == 0.1f)
            shownDef = 0;
        else
            shownDef-=0.1f;

        shownDefPrice -= defPricenum;
    }
    public void Purchase()
    {
        if(shownHp > playerCombat.playerHealth)
        {
            playerCombat.playerHealth = shownHp;
            playerCombat.currentHealth = shownHp;
            UpdateHealthBars();
        }
           
        if(shownDef>playerCombat.playerDefense)
            playerCombat.playerDefense = shownDef;
        if(shownAtk > playerCombat.attackDamage)
            playerCombat.attackDamage = shownAtk;

        characterController2D.moneyNum -= shownPrice;
    }
    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
    }
}
