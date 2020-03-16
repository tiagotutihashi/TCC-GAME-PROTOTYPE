using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {

    public static UIFade instance;

    public Image fadeImage;
    public float speed;

    private bool shouldFadeToBlack;
    private bool shouldFadeFromblack;

    void Start() {

        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

    }

    void Update() {

        if (shouldFadeToBlack) {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards(fadeImage.color.a, 1f, speed * Time.deltaTime));
            if(fadeImage.color.a == 1f) {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromblack) {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards(fadeImage.color.a, 0f, speed * Time.deltaTime));
            if(fadeImage.color.a == 0f) {
                shouldFadeFromblack = false;
            }
        }

    }

    public void FadeToBlack() {
        shouldFadeToBlack = true;
        shouldFadeFromblack = false;
    }

    public void FadeFromBlack() {
        shouldFadeToBlack = false;
        shouldFadeFromblack = true;
    }

}
