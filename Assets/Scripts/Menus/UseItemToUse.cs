using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemToUse : MonoBehaviour {

    public Button details;
    public Button[] charsB;
    public GameObject chars;
    public UseItem itemToUse;
    public Text[] charsBText;

    public void SetItemToUse(UseItem toUse) {
        itemToUse = toUse;
    }
    
    public void ChangeButtons() {

        TerminalManager.instance.ShowInTerminal("UseItemToUse.ChangeButtons()");

        details.gameObject.SetActive(false);
        chars.SetActive(true);

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

            charsBText[i].text = GameManager.instance.playerStats[i].charName;

            if ((GameManager.instance.playerStats[i].currentHP == GameManager.instance.playerStats[i].maxHP && itemToUse.hp != 0) || 
                (GameManager.instance.playerStats[i].currentEne == GameManager.instance.playerStats[i].maxEne && itemToUse.ene != 0)) {
       
                charsB[i].interactable = false; 
            } 
        }

    }

    public void BackDetails() {

        TerminalManager.instance.ShowInTerminal("UseItemToUse.BackDetails()");

        details.gameObject.SetActive(true);
        chars.SetActive(false);
    }

    public void UseItemOnChar(int charToUse) {

        TerminalManager.instance.ShowInTerminal("UseItemToUse.UseItemOnChar()");

        bool useItem = false;

        if (itemToUse.hp + GameManager.instance.playerStats[charToUse].currentHP >= GameManager.instance.playerStats[charToUse].maxHP) {
            GameManager.instance.playerStats[charToUse].currentHP = GameManager.instance.playerStats[charToUse].maxHP;
            useItem = true;
        } else {
            GameManager.instance.playerStats[charToUse].currentHP += itemToUse.hp;
            useItem = true;
        }

        if (itemToUse.ene + GameManager.instance.playerStats[charToUse].currentEne >= GameManager.instance.playerStats[charToUse].maxEne) {
            GameManager.instance.playerStats[charToUse].currentEne = GameManager.instance.playerStats[charToUse].maxEne;
            useItem = true;
        } else {
            GameManager.instance.playerStats[charToUse].currentEne += itemToUse.ene;
            useItem = true;
        }

        if (useItem) {

            ItemManager.instance.RemoveItem(itemToUse);
            BackDetails();
            ItemManager.instance.LoadItems();

        } else {

            BackDetails();
            
        }

    }

}
