using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTargetButton : MonoBehaviour {

    public string moveName;
    public int target;
    public Text targetName;
    public bool waitTime = false;

    void Start() {
        
    }

    void Update() {
        if (waitTime) {
            StartCoroutine(BattleManager.instace.PlayerAttack(moveName, target));
            waitTime = false;
        }
    }


    public void Press() {
        waitTime = true;
    }
}
