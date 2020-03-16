using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour {

    public Button refButton;
    public Sprite back;
    public Button backButton;

    public void ShowMoves() {

        List<UseItem> theList = GameManager.instance.GetPlayerUseItems();
        UseItem[] itemNames = GameManager.instance.useItems;
        
        for (int i = 0; i < theList.Count; i++) {
            Button newB = Instantiate(refButton, gameObject.transform);
            BattleItem newI = newB.GetComponentInChildren<BattleItem>();
            for (int f = 0; f < itemNames.Length; f++) {
                if (itemNames[f].itemName == theList[i].itemName) {
                    newI.theItem = theList[i];
                    newI.itemAmount = GameManager.instance.amountHaveUseItems[i];
                }
            }
        }

    }

    public void disableList() {

        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        BattleManager.instace.itemMenu.SetActive(false);
        BattleManager.instace.playerTargetMenu.SetActive(false);
    }

    public void CreateBack() {
        Button newB = Instantiate(backButton, gameObject.transform);
        newB.onClick.AddListener(delegate { disableList(); });
        newB.gameObject.transform.Find("Name").GetComponent<Text>().text = "Voltar";
        newB.gameObject.transform.Find("Image").GetComponent<Image>().sprite = back;
        newB.gameObject.transform.Find("Amount").GetComponent<Text>().text = "";
        newB.gameObject.transform.Find("Effect").GetComponent<Text>().text = "";
    }
}
