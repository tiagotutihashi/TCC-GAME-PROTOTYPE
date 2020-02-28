using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListAttack : MonoBehaviour {

    public Button refButton;
    public string[] movesNames;
    public static ListAttack instance;

    void Start() {
        instance = this;
        
    }

    void Update() {
        
    }

    public void ShowMoves() {

        for(int i = 0; i < movesNames.Length; i++) {
            Button newB = Instantiate(refButton, gameObject.transform);
            newB.GetComponentInChildren<Text>().text = movesNames[i];
        }

    }

}
