using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    public float waitToLoad;
    public string firstLevel;

    void Update() {
        
        if(waitToLoad > 0) {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0) {
                if (PlayerPrefs.HasKey("Current_Scene"))
                {
                    SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
                    GameManager.instance.LoadGame();
                } else
                {
                    SceneManager.LoadScene(firstLevel);
                }
            }
        }


    }

}
