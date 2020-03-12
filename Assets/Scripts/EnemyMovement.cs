using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public string enemyName;
    public int typeMovement;
    public Rigidbody2D rb;
    public float x;
    public float y;
    public bool showOnce = true;
    public float speed;
    public string[] enemiesToBattle = new string[2];

    void Start() {

        rb = GetComponent<Rigidbody2D>();
        x = 0f;
        y = 0f;

        TerminalManager.instance.ShowInTerminalObject(enemyName + " - EnemyMovement");

    }


    void Update() {

        if (GameManager.instance.canMove) {
            switch (typeMovement) {
                case 1:
                    FollowPlayer();
                    break;
                default:
                    StayInPosition();
                    break;
            }
        } else {
            if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < 3) {
                Vector3 player = PlayerController.instance.transform.position;
                if (Mathf.FloorToInt(player.x) > Mathf.FloorToInt(transform.position.x)) {
                    x = 1f;
                } else if (Mathf.FloorToInt(player.x) < Mathf.FloorToInt(transform.position.x)) {
                    x = -1f;
                } else {
                    x = 0f;
                }

                if (Mathf.FloorToInt(player.y) > Mathf.FloorToInt(transform.position.y)) {
                    y = 1f;
                } else if (Mathf.FloorToInt(player.y) < Mathf.FloorToInt(transform.position.y)) {
                    y = -1f;
                } else {
                    y = 0f;
                }
                rb.velocity = new Vector2(-x, -y);
            } else {
                rb.velocity = new Vector2(0, 0);
            }
        }

    }

    public void StayInPosition() {

        if (showOnce)
            TerminalManager.instance.ShowInTerminal(enemyName + ".SayInPosition()");

        showOnce = false;

        x = 0f;
        y = 0f;

        rb.velocity = new Vector2(x, y);

    }

    public void FollowPlayer() {

        if (showOnce)
            TerminalManager.instance.ShowInTerminal(enemyName + ".FollowPlayer()");

        showOnce = false;

        Vector3 player = PlayerController.instance.transform.position;

        if (Vector2.Distance(transform.position, player) > 1) {
            
            if (Mathf.FloorToInt(player.x) > Mathf.FloorToInt(transform.position.x)) {
                x = 1f;
            } else if (Mathf.FloorToInt(player.x) < Mathf.FloorToInt(transform.position.x)) {
                x = -1f;
            } else {
                x = 0f;
            }

            if (Mathf.FloorToInt(player.y) > Mathf.FloorToInt(transform.position.y)) {
                y = 1f;
            } else if (Mathf.FloorToInt(player.y) < Mathf.FloorToInt(transform.position.y)) {
                y = -1f;
            } else {
                y = 0f;
            }

            rb.velocity = new Vector2(x, y);

        } else {

            rb.velocity = new Vector2(0, 0);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (BattleManager.instace.nameEnemy != enemyName) {
                BattleManager.instace.BattleStart(enemiesToBattle, enemyName);
                Destroy(gameObject);
            }
        }
    }

}
