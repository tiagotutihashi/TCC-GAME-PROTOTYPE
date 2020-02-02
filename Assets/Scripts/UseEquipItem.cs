using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseEquipItem : MonoBehaviour {

    public Button details;
    public Button[] charsB;
    public GameObject chars;
    public EquipItem equipToUse;
    public Text[] charsBText;

    public void ChangeButtons() {

        TerminalManager.instance.ShowInTerminal("UseEquipItem.ChangeButtons()");

        bool alreadyEquiped = false;

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

            charsBText[i].text = GameManager.instance.playerStats[i].charName;

            if ((GameManager.instance.playerStats[i].weapon == equipToUse) ||
                (GameManager.instance.playerStats[i].armor == equipToUse)) {

                alreadyEquiped = true;

            }

        }

        if (alreadyEquiped) {

            for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

                charsB[i].interactable = false;

            }
            

        } else {
            details.gameObject.SetActive(false);
            chars.SetActive(true);
        }

    }

    public void BackDetails() {

        TerminalManager.instance.ShowInTerminal("UseEquipItem.BackDetails()");

        details.gameObject.SetActive(true);
        chars.SetActive(false);
    }

    public void EquipItemOnChar(int charToUse) {

        TerminalManager.instance.ShowInTerminal("UseEquipItem.EquipItemOnChar("+ charToUse +")");

        if (equipToUse.type == 0) {
            GameManager.instance.playerStats[charToUse].weapon = equipToUse;
        } else {
            GameManager.instance.playerStats[charToUse].armor = equipToUse;
        }

        BackDetails();

        EquipManager.instance.LoadEquip();

    }

}
