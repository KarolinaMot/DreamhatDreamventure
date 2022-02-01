using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class terminalCode : MonoBehaviour
{

    public PlayerCombat playerCombat;
    public GameObject textOpen;
    public GameObject terminalTable;
    public UICode uICode;
    bool textShown = false;

    [Header("Audio")]
    [SerializeField] AudioSource menuButton;
    [SerializeField] AudioSource moneyButton;
    [SerializeField] AudioSource computerSound;
    
    [Header("Text Fields")]
    [SerializeField] private Text currentAtkText;
    [SerializeField] private Text currentHpText;
    [SerializeField] private Text currentDefText;
    [SerializeField] private Text atkPrice;
    [SerializeField] private Text hpPrice;
    [SerializeField] private Text defPrice;
    [SerializeField] private Text fullPrice;

    [Header("Buttons")]
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


    void Awake()
    {
        shownHp = playerCombat.totalLifes;
        shownDef = playerCombat.currentDef;
        shownAtk = playerCombat.currentAtk;
    }
    // Start is called before the first frame update
    void Update()
    {
        DisableButtons();

        if (textShown && Input.GetKey(KeyCode.T))
        {
            computerSound.Play();
            terminalTable.SetActive(true);
            textOpen.SetActive(false);
        }

        currentHpText.text = shownHp.ToString();
        currentAtkText.text = shownAtk.ToString();
        currentDefText.text = (shownDef * 10f).ToString();

        atkPrice.text = shownAtkPrice.ToString();
        defPrice.text = shownDefPrice.ToString();
        hpPrice.text = shownHpPrice.ToString();
        shownPrice = (shownAtkPrice + shownDefPrice + shownHpPrice);
        fullPrice.text = shownPrice.ToString();

        if (shownPrice > playerCombat.currentCoins)
            purchase.interactable = false;
        else
            purchase.interactable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "GoodPlayer" || collision.gameObject.name == "BadPlayer")
        {
            textOpen.SetActive(true);
            textShown = true;
        }
        Debug.Log(collision.gameObject.name);
    }

    void DisableButtons()
    {
        if (shownHp <= playerCombat.totalLifes)
        {
            hpDown.interactable = false;
        }
        else
        {
            hpDown.interactable = true;
        }

        if (shownHp >= maxHp)
        {
            hpUp.interactable = false;
        }
        else
        {
            hpUp.interactable = true;
        }

        if (shownAtk <= playerCombat.currentAtk)
        {
            atkDown.interactable = false;
        }
        else
        {
            atkDown.interactable = true;
        }

        if (shownAtk >= maxAtk)
        {
            atkUp.interactable = false;
        }
        else
        {
            atkUp.interactable = true;
        }


        if (shownDef <= playerCombat.currentDef)
        {
            defDown.interactable = false;
        }
        else
        {
            defDown.interactable = true;
        }

        if (shownDef >= maxDef)
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
        menuButton.Play();
    }
    public void AtkPressUp()
    {
        shownAtk++;
        shownAtkPrice += atkPricenum;
        menuButton.Play();
    }
    public void DefPressUp()
    {
        shownDef += 0.1f;
        shownDefPrice += defPricenum;
        menuButton.Play();
    }
    public void HealthPressDown()
    {
        shownHp--;
        shownHpPrice -= defPricenum;
        menuButton.Play();
    }
    public void AtkPressDown()
    {
        shownAtk--;
        shownAtkPrice -= atkPricenum;
        menuButton.Play();
    }
    public void DefPressDown()
    {
        menuButton.Play();
        if (shownDef == 0.1f)
            shownDef = 0;
        else
            shownDef -= 0.1f;

        shownDefPrice -= defPricenum;

    }
    public void Purchase()
    {
        menuButton.Play();
        moneyButton.Play();
        if (shownHp > playerCombat.totalLifes)
        {
            playerCombat.totalLifes = shownHp;
            playerCombat.totalLifes = shownHp;
            uICode.UpdateHealthBars();
        }

        if (shownDef > playerCombat.currentDef)
            playerCombat.currentDef = shownDef;
        if (shownAtk > playerCombat.currentAtk)
            playerCombat.currentAtk = shownAtk;

        playerCombat.currentCoins -= shownPrice;
    }
    public void ClosePanel()
    {
        terminalTable.SetActive(false);
        menuButton.Play();
    }

}
