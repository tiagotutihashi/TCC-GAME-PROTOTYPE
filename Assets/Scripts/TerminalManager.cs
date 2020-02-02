using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour {

    public static TerminalManager instance;

    public Text line;

    void Start() {

        instance = this;

    }

    public void ShowInTerminal(string newLine) {

        line.text = line.text.Insert(0, newLine + "\n");
        
    }

}
