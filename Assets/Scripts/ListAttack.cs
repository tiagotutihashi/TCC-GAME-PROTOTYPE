using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListAttack : MonoBehaviour {

    public Button refButton;
    public string[] movesNames;
    public int energy;

    public void ShowMoves() {

        for(int i = 0; i < movesNames.Length; i++) {
            Button newB = Instantiate(refButton, gameObject.transform);
            SpecialMove newM = newB.GetComponentInChildren<SpecialMove>();
            newM.moveName = movesNames[i];
            newM.energy = energy; 
        }

    }

    public void disableList() {

        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        BattleManager.instace.ListAttackMenu.SetActive(false);
        BattleManager.instace.targetMenu.SetActive(false);
    }

    public void CreateBack() {
        Button newB = Instantiate(refButton, gameObject.transform);
        newB.onClick.AddListener(delegate { disableList(); });
        newB.gameObject.transform.Find("Name").GetComponent<Text>().text = "Voltar";
        newB.gameObject.transform.Find("Power").GetComponent<Text>().text = "";
        newB.gameObject.transform.Find("Cost").GetComponent<Text>().text = "";
        newB.gameObject.transform.Find("Cost Value").GetComponent<Text>().text = "";
        newB.gameObject.transform.Find("Power Value").GetComponent<Text>().text = "";
    }

}
