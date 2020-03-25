using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour {

    public static TerminalManager instance;

    public Text line;
    public Text obj;

    void Start() {

        instance = this;

        ShowInTerminalObject("TerminalManager - TerminalManager");

    }

    public void ShowInTerminalObject( string newObj ) {

        obj.text = obj.text.Insert(0, newObj + "\n");

    }

    public void ShowInTerminal( string newLine ) {

        line.text = line.text.Insert(0, newLine + "\n");

    }

    public void EraserObject() {

        string constants = "BattleManager - BattleMaanger\nGameManager - GameManager\nTerminalManager - TerminalManager\nUIcanvas - UICanvas\nPlayerController - PlayerController\n";
        obj.text = constants;
        line.text = "";

    }

}
