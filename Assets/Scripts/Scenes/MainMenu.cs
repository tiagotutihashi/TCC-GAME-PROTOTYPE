﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string newGameScene;
    public GameObject continueButton;
    public string loadGameScene;

    void Start() {

        if (PlayerPrefs.HasKey("Current_Scene")) {
            continueButton.SetActive(true);
        } else {
            continueButton.SetActive(false);
        }

    }

    void Update() {
        
    }

    public void ContinueGame() {

        SceneManager.LoadScene(loadGameScene);

    }

    public void NewGame() {

        SceneManager.LoadScene(loadGameScene);

    }

    public void ExitGame() {

        Application.Quit();

    }

}
