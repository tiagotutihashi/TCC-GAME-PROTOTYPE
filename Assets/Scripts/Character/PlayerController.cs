using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public Rigidbody2D rigid;
    public Animator anim;

    public float x, y, speed;
    public string areaTransitionName;
    public bool canMove = true;

    private bool stop;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    void Start() {

        if (instance == null) {
            instance = this;
        } else {
            if(instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        x = 0f;
        y = 0f;

    }

    void Update() {

        if (canMove) {
            Movement();
        } else {
            rigid.velocity = Vector2.zero;
        }
        
    }

    private void Movement() {

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if(x != 0 || y!= 0) {
            if(stop)
                TerminalManager.instance.ShowInTerminal("PlayerController.Movement()");
            stop = false;
        } else {
            stop = true;
        }

        rigid.velocity = new Vector2(x, y) * speed;

        anim.SetFloat("moveX", x);
        anim.SetFloat("moveY", y);

        if (x == 1 || x == -1 || y == 1 || y == -1) {
            anim.SetFloat("lastMoveX", x);
            anim.SetFloat("lastMoveY", y);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight) {

        TerminalManager.instance.ShowInTerminal("PlayerController.SetBounds((" + botLeft.x + ", " + botLeft.y + ", " + botLeft.z + ") (" + topRight.x + ", " + topRight.y + ", " + topRight.z + "))");

        bottomLeftLimit = botLeft + new Vector3(0.5f, 0.5f, 0f);
        topRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0f);

    }

}
