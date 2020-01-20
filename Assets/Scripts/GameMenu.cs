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

        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++) {

            if (playerStats[i].gameObject.activeInHierarchy) {

                charStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                eneText[i].text = "Energia: " + playerStats[i].currentEne + "/" + playerStats[i].maxEne;
                lvlText[i].text = "Level: " + playerStats[i].level;
                //expText[i].text = "Experiência: " + playerStats[i].currentExp;
                expSlider[i].maxValue = playerStats[i].exptToLevelUp[playerStats[i].level];
                expSlider[i].value = playerStats[i].currentExp;
                charImage[i].sprite = playerStats[i].charImage;
                wpnText[i].text = "Arma: " + playerStats[i].wpnName;
                armText[i].text = "Armadura: " + playerStats[i].armName;

            } else {
                charStatHolder[i].SetActive(false);
            }

        }

    }

    public void CloseMenu() {

        for (int i = 0; i < windows.Length; i++) {

            windows[i].SetActive(false);

        }

        menu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;

    }

    public void OpenMenu() {

        windows[0].SetActive(true);

        menu.SetActive(true);

        GameManager.instance.gameMenuOpen = true;

        UpdateMainStats();

    }

    public void ToggleWindow(int winNumber) {

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

}
