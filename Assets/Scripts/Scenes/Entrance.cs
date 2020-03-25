using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

    public string transitionName;
    public string areaToLoad;

    void Start() {

        if(transitionName == PlayerController.instance.areaTransitionName) {
            PlayerController.instance.transform.position = transform.position;
        }

        UIFade.instance.FadeFromBlack();

        GameManager.instance.fadingBetweenAreas = false;

        TerminalManager.instance.ShowInTerminalObject(areaToLoad + " - Exit");
        TerminalManager.instance.ShowInTerminalObject(transitionName + " - Entrance");

    }
   
}
