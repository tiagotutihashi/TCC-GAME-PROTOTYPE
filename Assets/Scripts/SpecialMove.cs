using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMove : MonoBehaviour {

    public string moveName;
    public int moveCost;
    public int movePower;
    public int energy;
    public Text moveNameText;
    public Text moveCostText;
    public Text movePowerText;
    public BattleMove theMove;

    void Start() {
        for(int i = 0; i < BattleManager.instace.movesList.Length; i++) {
            if(BattleManager.instace.movesList[i].moveName == moveName) {
                theMove = BattleManager.instace.movesList[i];
                moveNameText.text = moveName;
                moveCost = BattleManager.instace.movesList[i].moveCost;
                moveCostText.text = moveCost.ToString();
                movePower = BattleManager.instace.movesList[i].movePower;
                movePowerText.text = movePower.ToString();
            }
            if(moveCost > energy) {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void Press() {
        BattleManager.instace.OpenTargetMenu(moveName);
    }
}
