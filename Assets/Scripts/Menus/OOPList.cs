using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OOPList : MonoBehaviour {

    public Button buttonRef;
    public GameObject[] oopList;

    void Start() {

    }

    void Update() {

    }

    public void ShowOOPList() {

        for(int i = 0; i < oopList.Length; i++) {

            Button newB = Instantiate(buttonRef, gameObject.transform);
            newB.GetComponentInChildren<Text>().text = "";

        }

    }

}
