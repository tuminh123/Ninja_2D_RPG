using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    [Header("Bar")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;

    [Header("TextTMP")]
    //player ui text
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI healthAmountTMP;
    [SerializeField] private TextMeshProUGUI manaAmountTMP;
    [SerializeField] private TextMeshProUGUI expAmountTMP;
    [SerializeField] private TextMeshProUGUI coinAmountTMP;
    //stats text
    [Header("Stats Panel")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI statLevelTMP;
    [SerializeField] private TextMeshProUGUI statDamageTMP;
    [SerializeField] private TextMeshProUGUI statCChaneTMP;
    [SerializeField] private TextMeshProUGUI statCDamageTMP;
    [SerializeField] private TextMeshProUGUI statTotalExpTMP;
    [SerializeField] private TextMeshProUGUI statCurrentExpTMP;
    [SerializeField] private TextMeshProUGUI statRequiredExpTMP;
    // Attribute text
    [SerializeField] private TextMeshProUGUI pointAttributeTMP;
    [SerializeField] private TextMeshProUGUI strengthTMP;
    [SerializeField] private TextMeshProUGUI dexterityTMP;
    [SerializeField] private TextMeshProUGUI intelligenceTMP;

    [Header("Extra Panel")]
    [SerializeField] private GameObject npcQuestPanel;
    [SerializeField] private GameObject playerQuestPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject craftPanel;

    private void Awake()
    {
        statsPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdatePlayerUI();
    }

    private void OnEnable() {
        PlayerUpgrade.OnPlayerUpgradeEvent += UpgradeCallBack;
        DialogueManager.OnExtraInteractionEvent += ExtraInteractionCallBack;
    }
    private void OnDisable() {
        PlayerUpgrade.OnPlayerUpgradeEvent -= UpgradeCallBack;
        DialogueManager.OnExtraInteractionEvent -= ExtraInteractionCallBack;
    }

    private void ExtraInteractionCallBack(InteractionType interactionType)
    {
        switch (interactionType) 
        {
            case InteractionType.Quest:
                OpenCloseNPCQuestPanel(true);
                break;
            case InteractionType.Shop:
                OpenCloseShopPanel(true);   
                break;
            case InteractionType.Craft:
                OpenCloseCraftPanel(true);
                break;
        }
    }

    private void UpgradeCallBack()
    {
        UpdateStatsPanel();
    }
    //Shop 
    public void OpenCloseShopPanel(bool value)
    {
        shopPanel.SetActive(value);
    }

    //npc quest
    public void OpenCloseNPCQuestPanel(bool value)
    {
        npcQuestPanel.SetActive(value);
    }
    //player quest
    public void OpenClosePlayerQuestPanel(bool value)
    {
        playerQuestPanel.SetActive(value);
    }
    //craft
    public void OpenCloseCraftPanel(bool value)
    {
        craftPanel.gameObject.SetActive(value);
    }

    //UI player
    private void UpdatePlayerUI()
    {
        //update bar image
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.currentHealth / stats.maxHealth, 10f * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.currentMana / stats.maxMana, 10f * Time.deltaTime);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.currentExp / stats.nextLevelExp, 10f * Time.deltaTime);

        //update text amount and level
        levelTMP.text = $"Level {stats.level}";
        healthAmountTMP.text = $"{stats.currentHealth} / {stats.maxHealth}";
        manaAmountTMP.text = $"{stats.currentMana} / {stats.maxMana}";
        expAmountTMP.text = $"{stats.currentExp} / {stats.nextLevelExp}";
        coinAmountTMP.text = CoinManager.Instance.Coins.ToString();
    }

    //UI Stats Player
    public void OpenCloseStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if (statsPanel.activeSelf)
        {
            UpdateStatsPanel();
        }
    }

    private void UpdateStatsPanel()
    {
        statLevelTMP.text = stats.level.ToString();
        statDamageTMP.text = stats.totalDamage.ToString();
        statCChaneTMP.text = stats.criticalChance.ToString();
        statCDamageTMP.text = stats.criticalDamage.ToString();
        statTotalExpTMP.text = stats.totalExp.ToString();
        statCurrentExpTMP.text = stats.currentExp.ToString();
        statRequiredExpTMP.text = stats.nextLevelExp.ToString();

        // Attribute update
        pointAttributeTMP.text = $"Points: {stats.attributePoint}";
        strengthTMP.text = stats.strength.ToString();
        dexterityTMP.text = stats.dexterity.ToString();
        intelligenceTMP.text = stats.intelligence.ToString();

    }


}
