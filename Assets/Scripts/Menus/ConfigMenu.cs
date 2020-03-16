using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigMenu : MonoBehaviour {

    private bool goToMainMenu = false;

    void Update() {

        if (goToMainMenu) {
            StartCoroutine(GoToMainMenu());
        }

    }

    public void ExitGame() {

        TerminalManager.instance.ShowInTerminal("ConfigMenu.ExitGame()");

        Application.Quit();

    }

    public void SaveGame() {

        TerminalManager.instance.ShowInTerminal("ConfigMenu.SaveGame()");

        GameManager.instance.SaveGame();

    }

    public void MainMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.MainMenu()");

        goToMainMenu = true;

    }

    public IEnumerator GoToMainMenu() {

        TerminalManager.instance.ShowInTerminal("ConfigMenu.GoToMainMenu()");

        UIFade.instance.FadeFromBlack();
        yield return new WaitForSeconds(.1f);
        GameManager.instance.DestroyAllnGoToMainMenu();

    }

}
