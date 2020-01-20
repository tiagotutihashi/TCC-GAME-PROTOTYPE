﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    public string areaToLoad;
    public string areaTransitionName;

    public Entrance theEnt;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    void Start() {
        theEnt.transitionName = areaTransitionName;
    }

    void Update() {
        if (shouldLoadAfterFade) {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0){
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D obj) {

        if(obj.tag == "Player") {
   
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();
            PlayerController.instance.areaTransitionName = areaTransitionName;

        }

    }

}
