using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItem : MonoBehaviour {

    public int itemAmount;
    public Image imageSprite;
    public Text nameText;
    public Text effectText;
    public Text amountText;
    public UseItem theItem;

    void Start() {

        string effect = "";
        if (theItem != null) {
            imageSprite.sprite = theItem.image;
            nameText.text = theItem.itemName;
            amountText.text = itemAmount.ToString();
            if (theItem.hp > 0) {
                effect += "HP : " + theItem.hp + "\n";
            }
            if (theItem.ene > 0) {
                effect += "Ene: " + theItem.ene;
            }
            effectText.text = effect;

        }

    }

    public void Press() {
        BattleManager.instace.OpenPlayerTargetMenu(theItem);
    }

}
