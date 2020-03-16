using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;
    public GameObject battleMan;
    public GameObject playerCamera;

    void Start() {

        LoadGameObjectInScene();

    }

    public void LoadGameObjectInScene() {

        if (UIFade.instance == null) {
            //UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
            Instantiate(UIScreen);
        }

        if (GameManager.instance == null) {
            Instantiate(gameMan);
        }

        if(BattleManager.instace == null) {
            Instantiate(battleMan);
        }

        if (PlayerController.instance == null) {
            //PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
            Instantiate(player);
        }

        if (CameraController.instance == null) {
            Instantiate(playerCamera);
        }

    }

}
