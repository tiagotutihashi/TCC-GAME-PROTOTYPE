using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTarget : MonoBehaviour {

    public string moveName;
    public UseItem item;
    public int target;
    public Text targetName;
    public bool waitTime = false;

    void Start() {

    }

    void Update() {
        if (waitTime) {
            StartCoroutine(BattleManager.instace.PlayerUseItem(item, target));
            waitTime = false;
        }
    }

    public void Press() {
        waitTime = true;
    }
}
