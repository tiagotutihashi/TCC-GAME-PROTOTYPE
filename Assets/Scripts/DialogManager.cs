using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;
    public int currentLine;
    private bool justStarted;

    public static DialogManager instance;

    void Start() {

        instance = this;

        dialogText.text = dialogLines[currentLine];

    }

    void Update() {

        if (dialogBox.activeInHierarchy) {
            if (Input.GetButtonUp("Fire1")) {

                if (!justStarted) { 

                    currentLine++;
                    

                    if(dialogLines.Length <= currentLine) {
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogActive = false;
                    } else {
                        CheckIfName();
                        dialogText.text = dialogLines[currentLine];
                    }

                } else {

                    justStarted = false;

                }


            }
        }
        
    }

    public void ShowDialog(string[] newLines, bool isPerson) {

        TerminalManager.instance.ShowInTerminal("DialogManager.ShowDialog("+newLines+", "+isPerson+")");

        dialogLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(true);

        justStarted = true;

        nameBox.SetActive(isPerson);

        GameManager.instance.dialogActive = true;

    }

    public void CheckIfName() {

        TerminalManager.instance.ShowInTerminal("DialogManager.CheckIfName()");

        if (dialogLines[currentLine].StartsWith("n-")) {

            nameText.text = dialogLines[currentLine].Replace("n-","");
            currentLine++;

        }

    }

}
