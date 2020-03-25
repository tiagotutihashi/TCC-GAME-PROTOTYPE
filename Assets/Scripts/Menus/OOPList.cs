using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OOPList : MonoBehaviour {

    public Button buttonRef;
    public GameObject theList;
    public GameObject showIn;

    public GameObject[] oppConcepts;

    void Start() {
        
    }

    void Update() {

    }

    public void ButtonAction() {

        theList.SetActive(false);

    }

    public void ShowOOPList() {

        for(int i = 0; i < GameManager.instance.learnedConcepts.Count; i++) {

            Button newB = Instantiate(buttonRef, gameObject.transform);
            newB.GetComponentInChildren<Text>().text = GameManager.instance.learnedConcepts[i];

        }

    }

}
