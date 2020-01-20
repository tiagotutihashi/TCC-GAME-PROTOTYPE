﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;

    void Start() {
        
        if(UIFade.instance == null) {
            UIFade.instance =  Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if(PlayerController.instance == null) {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        if(GameManager.instance == null) {
            Instantiate(gameMan);
        }

    }

    void Update() {
        
    }

}
