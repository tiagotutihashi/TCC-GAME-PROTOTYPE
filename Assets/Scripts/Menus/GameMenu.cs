using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public GameObject menu;
    public GameObject[] windows;
    public Button[] btnOpen, btnClose;

    private CharacterStats[] playerStats;

    public Text[] nameText,
        hpText, eneText, lvlText, expText, wpnText, armText;
    public Slider[] expSlider, hpSlider, eneSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public Button[] btnOpenStatus, btnCloseStatus;
    public Button[] btnOpenItems, btnCloseItems;

    [Header("Status Menu")]
    public Text stsHP, stsEne, stsLvl, stsExp, 
        stsAtt, stsDef, stsSPAtt, stsSPDef, 
        stsWpn, stsArm;

    public ScrollRect[] itensWin;

    public GameObject configMenu;
    public GameObject oppPanel;
    public GameObject confNopp;
    public Button saveButton;
    public GameObject configButtons;
    private bool configMenuOpen = false;
    private bool menuOpen = false;

    void Start() {
        
    }

    void Update() {

        if (Input.GetButtonDown("Fire2")) {
            if (menu.activeInHierarchy) {
                CloseMenu();
            } else {
                OpenMenu();
            }
        }

    }

    public void UpdateMainStats() {

        TerminalManager.instance.ShowInTerminal("GameMenu.UpdateMainStats()");

        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++) {

            if (playerStats[i].gameObject.activeInHierarchy) {

                charStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                hpSlider[i].maxValue = playerStats[i].maxHP; ;
                hpSlider[i].value = playerStats[i].currentHP;
                eneText[i].text = "Energia: " + playerStats[i].currentEne + "/" + playerStats[i].maxEne;
                eneSlider[i].maxValue = playerStats[i].maxEne;
                eneSlider[i].value = playerStats[i].currentEne;
                lvlText[i].text = "Level: " + playerStats[i].level;
                //expText[i].text = "Experiência: " + playerStats[i].currentExp;
                expSlider[i].maxValue = playerStats[i].exptToLevelUp[playerStats[i].level];
                expSlider[i].value = playerStats[i].currentExp;
                charImage[i].sprite = playerStats[i].charImage;
                wpnText[i].text = "Arma: " + playerStats[i].weapon.itemName;
                armText[i].text = "Armadura: " + playerStats[i].armor.itemName;
            } else {
                charStatHolder[i].SetActive(false);
            }

        }

    }

    public void CloseMenu() {

        TerminalManager.instance.ShowInTerminal("GameMenu.CloseMenu()");

        for (int i = 0; i < windows.Length; i++) {

            windows[i].SetActive(false);

        }

        menu.SetActive(false);
        menuOpen = false;
        configButtons.SetActive(true);

        GameManager.instance.gameMenuOpen = false;

    }

    public void OpenMenu() {

        if (!configMenuOpen) {
            TerminalManager.instance.ShowInTerminal("GameMenu.OpenMenu()");
            windows[0].SetActive(true);
            menu.SetActive(true);
            menuOpen = true;
            configButtons.SetActive(false);
            GameManager.instance.gameMenuOpen = true;
            ToggleWindow(0);
        }

    }

    public void ToggleWindow(int winNumber) {

        TerminalManager.instance.ShowInTerminal("GameMenu.ToggleWindow("+ winNumber + ")");

        UpdateMainStats();

        for(int i = 0; i < windows.Length; i++) {

            if(i == winNumber) {
                windows[i].SetActive(true);
                btnOpen[i].gameObject.SetActive(true);
            } else {
                windows[i].SetActive(false);
                btnClose[i].gameObject.SetActive(true);
                btnOpen[i].gameObject.SetActive(false);
            }

        }

    }

    public void StatusMenu(int charNumber) {

        TerminalManager.instance.ShowInTerminal("GameMenu.StatusMenu(" + charNumber + ")");

        UpdateMainStats();

        for(int i = 0; i < btnOpenStatus.Length; i++) {

            if (i == charNumber) {

                btnOpenStatus[i].gameObject.SetActive(true);

                string name = "";

                foreach (char c in playerStats[i].charName) {
                    name += c + "\n";
                }
                btnOpenStatus[i].gameObject.GetComponentInChildren<Text>().text = name;
                UpdateCharStatus(playerStats[i]);
            } else {
                btnCloseStatus[i].gameObject.SetActive(true);
                string name = "";

                foreach (char c in playerStats[i].charName) {
                    name += c + "\n";
                }
                btnCloseStatus[i].gameObject.GetComponentInChildren<Text>().text = name;
                btnOpenStatus[i].gameObject.SetActive(false);
            }

        }

    }

    private void UpdateCharStatus(CharacterStats status) {

        TerminalManager.instance.ShowInTerminal("MenuManager.UpdateCharStatus("+ status.charName +")");

        stsLvl.text = status.level.ToString();
        stsExp.text = status.currentExp.ToString() + "/" + status.exptToLevelUp[status.level];
        stsHP.text = status.currentHP.ToString() + "/" + status.maxHP;
        stsEne.text = status.currentEne.ToString() + "/" + status.maxEne;
        stsAtt.text = (status.attack + status.weapon.att).ToString();
        stsSPAtt.text = status.spAttack.ToString();
        stsDef.text = (status.defense + status.armor.def).ToString();
        stsSPDef.text = status.spDefense.ToString();
        stsWpn.text = status.weapon.itemName + " - " + status.weapon.att;
        stsArm.text = status.armor.itemName + " - " + status.armor.def;

    }

    public void ItemMenu(int itemNumber) {

        TerminalManager.instance.ShowInTerminal("MenuManager.ItemMenu(" + itemNumber + ")");

        for (int i = 0; i < btnOpenItems.Length; i++) {

            if (i == itemNumber) {

                btnOpenItems[i].gameObject.SetActive(true);
                itensWin[i].gameObject.SetActive(true);

            } else {
                btnCloseItems[i].gameObject.SetActive(true);
                btnOpenItems[i].gameObject.SetActive(false);
                itensWin[i].gameObject.SetActive(false);
            }

        }

    }

    public void OpenConfMenu() {

        if (!menuOpen) {
            TerminalManager.instance.ShowInTerminal("MenuManager.OpenConfigMenu()");
            GameManager.instance.gameMenuOpen = true;
            oppPanel.SetActive(false);
            confNopp.SetActive(true);
            configMenu.SetActive(true);
            if (GameManager.instance.canMove) {
                saveButton.interactable = true;
            } else {
                saveButton.interactable = false;
            }
            configMenuOpen = true;
        }

    }

    public void CloseConfMenu() {

        TerminalManager.instance.ShowInTerminal("MenuManager.CloseConfigMenu()");
        GameManager.instance.gameMenuOpen = false;
        oppPanel.SetActive(false);
        confNopp.SetActive(false);
        configMenu.SetActive(false);
        configMenuOpen = false;

    }

    public void OpenOPPList() {

        if (!menuOpen) {
            TerminalManager.instance.ShowInTerminal("MenuManager.OpenOPPList()");
            GameManager.instance.gameMenuOpen = true;
            confNopp.SetActive(true);
            configMenu.SetActive(false);
            oppPanel.SetActive(true);
            configMenuOpen = true;
        }

    }

    public void CloseOPPList() {

        TerminalManager.instance.ShowInTerminal("MenuManager.CloseOPPList()");
        GameManager.instance.gameMenuOpen = false;
        oppPanel.SetActive(false);
        confNopp.SetActive(false);
        configMenu.SetActive(false);
        configMenuOpen = false;

    }

}
