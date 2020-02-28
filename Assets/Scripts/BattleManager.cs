using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    public static BattleManager instace;

    private bool battleActive;

    public GameObject battleScene;

    public GameObject[] playerPosition;
    public CharacterStats[] enemyPosition;

    public CharacterStats[] enemies;

    public int currentTurn;
    public bool turnWaiting;

    public Text[] playName, playhp, playEne, playLevel;
    public Slider[] playSliderHp, playSliderEne;

    public Text[] eneName, enehp, eneEne, eneLevel;
    public Slider[] eneSliderHp, eneSliderEne;

    public BattleMove[] movesList;

    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;
    public GameObject PlayerMenu;
    public GameObject ListAttackMenu;

    void Start() {

        instace = this;
        DontDestroyOnLoad(gameObject);

    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.P)) {
            BattleStart(new string[] { "Snake", "Two Legs" });
            UpdateStatus();
        }

        if (battleActive) {
            if (turnWaiting) {
                if (currentTurn > 1) {
                    //Enemy Attack
                    PlayerMenu.SetActive(false);
                    StartCoroutine(EnemyMoveCo());
                } else {
                    PlayerMenu.SetActive(true);
                    //Player Attack
                }
            }
        }


    }

    public void UpdateStatus() {

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

            CharacterStats playChar = GameManager.instance.playerStats[i];

            playName[i].text = playChar.charName;
            playLevel[i].text = playChar.level.ToString();
            playhp[i].text = playChar.currentHP.ToString() + "/" + playChar.maxHP.ToString();
            playEne[i].text = playChar.currentEne.ToString() + "/" + playChar.maxEne.ToString();
            playSliderHp[i].maxValue = playChar.maxHP;
            playSliderHp[i].value = playChar.currentHP;
            playSliderEne[i].maxValue = playChar.maxEne;
            playSliderEne[i].value = playChar.currentEne;

        }

        for (int i = 0; i < enemyPosition.Length; i++) {

            CharacterStats playChar = enemyPosition[i];

            eneName[i].text = playChar.charName;
            eneLevel[i].text = playChar.level.ToString();
            enehp[i].text = playChar.currentHP.ToString() + "/" + playChar.maxHP.ToString();
            eneEne[i].text = playChar.currentEne.ToString() + "/" + playChar.maxEne.ToString();
            eneSliderHp[i].maxValue = playChar.maxHP;
            eneSliderHp[i].value = playChar.currentHP;
            eneSliderEne[i].maxValue = playChar.maxEne;
            eneSliderEne[i].value = playChar.currentEne;

        }

    }

    public void BattleStart(string[] enemeisToBattle) {

        if (!battleActive) {
            battleActive = true;

            GameManager.instance.battleActive = true;
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);

            for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

                playerPosition[i].GetComponentInChildren<SpriteRenderer>().sprite = GameManager.instance.playerStats[i].charImage;

            }

            for (int i = 0; i < enemyPosition.Length; i++) {

                for (int f = 0; f < enemies.Length; f++) {

                    if (enemies[f].charName == enemeisToBattle[i]) {
                        enemyPosition[i].GetComponentInChildren<SpriteRenderer>().sprite = enemies[f].charImage;
                        enemyPosition[i].charName = enemies[f].charName;
                        enemyPosition[i].maxHP = enemies[f].maxHP;
                        enemyPosition[i].currentHP = enemies[f].currentHP;
                        enemyPosition[i].maxEne = enemies[f].maxEne;
                        enemyPosition[i].currentEne = enemies[f].currentEne;
                        enemyPosition[i].level = enemies[f].level;
                        enemyPosition[i].attack = enemies[f].attack;
                        enemyPosition[i].defense = enemies[f].defense;
                        enemyPosition[i].spAttack = enemies[f].spAttack;
                        enemyPosition[i].spDefense = enemies[f].spDefense;
                        enemyPosition[i].moveAvailable = enemies[f].moveAvailable;
                    }

                }

            }

            turnWaiting = true;
            currentTurn = 0;

        }

    }

    public void NextTurn() {

        currentTurn++;
        if (currentTurn > 3) {
            currentTurn = 0;
        }
        turnWaiting = true;
    }

    public void UpdateBattle() {

        bool allEnemiesDead = false;
        bool allPlayersDead = false;
        int playCount = GameManager.instance.playerStats.Length;

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

            if (GameManager.instance.playerStats[i].currentHP < 0) {
                GameManager.instance.playerStats[i].currentHP = 0;
            }

            if (GameManager.instance.playerStats[i].currentHP == 0) {
                playCount--;
            }

        }

        if (playCount <= 0) {
            allPlayersDead = true;
        }

        int enemyCount = enemyPosition.Length;

        for (int i = 0; i < enemyPosition.Length; i++) {
            if (enemyPosition[i].currentHP < 0) {
                enemyPosition[i].currentHP = 0;
            }

            if (enemyPosition[i].currentHP == 0) {
                enemyCount--;
            }

            if (enemyCount <= 0) {
                allEnemiesDead = true;
            }
        }

        UpdateStatus();

        if (allEnemiesDead || allPlayersDead) {
            if (allEnemiesDead) {

            } else {

            }

            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;
        } else {
            for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {
                if (GameManager.instance.playerStats[i].currentHP == 0 && currentTurn < 2) {
                    currentTurn++;
                }
            }
            for (int i = 0; i < enemyPosition.Length; i++) {
                if (enemyPosition[i].currentHP == 0 && currentTurn > 1) {
                    currentTurn++;
                    if (currentTurn > 3) {
                        currentTurn = 0;
                    }
                }
            }
        }

    }

    public IEnumerator EnemyMoveCo() {

        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        int selectPlayer = Random.Range(0, GameManager.instance.playerStats.Length);

        if (GameManager.instance.playerStats[selectPlayer].currentHP <= 0) {
            if (selectPlayer == 0) {
                selectPlayer = 1;
            } else {
                selectPlayer = 0;
            }
        }

        int movePower = 0;
        int selectAttack = Random.Range(0, enemyPosition[currentTurn - 2].moveAvailable.Length);

        for (int i = 0; i < movesList.Length; i++) {
            if (movesList[i].moveName == enemyPosition[currentTurn - 2].moveAvailable[selectAttack]) {
                Instantiate(movesList[i].theEffect, playerPosition[selectPlayer].transform.position, playerPosition[selectPlayer].transform.rotation);
                movePower = movesList[i].movePower;
                yield return new WaitForSeconds(movesList[i].theEffect.effectLenght);
            }
        }

        DealDamage(selectPlayer, movePower);
        yield return new WaitForSeconds(1f);
        NextTurn();

    }

    public void DealDamage(int target, int movePower) {

        float attPwr = enemyPosition[currentTurn - 2].attack;
        float defPwr = GameManager.instance.playerStats[target].defense;

        float damage = (movePower * attPwr) / defPwr;

        int damageToGive = Mathf.RoundToInt(damage);

        GameManager.instance.playerStats[target].currentHP -= damageToGive;

        UpdateBattle();

    }

    public void DealDamageEnemy(int target, int movePower) {

        float attPwr = GameManager.instance.playerStats[currentTurn].attack;
        float defPwr = enemyPosition[target].defense;

        float damage = (movePower * attPwr) / defPwr;

        int damageToGive = Mathf.RoundToInt(damage);

        enemyPosition[target].currentHP -= damageToGive;

        UpdateBattle();

    }

    public IEnumerator PlayerAttack(string moveName, int selectTarget) {
        int movePower = 0;
        PlayerMenu.SetActive(false);
        for (int i = 0; i < movesList.Length; i++) {
            if (movesList[i].moveName == moveName) {
                Instantiate(movesList[i].theEffect, enemyPosition[selectTarget].transform.position, enemyPosition[selectTarget].transform.rotation);
                movePower = movesList[i].movePower;

                yield return new WaitForSeconds(movesList[i].theEffect.effectLenght);
            }
        }

        DealDamageEnemy(selectTarget, movePower);
        NextTurn();
        targetMenu.SetActive(false);

    }

    public void OpenTargetMenu(string moveName) {

        targetMenu.SetActive(true);

        List<int> Enemies = new List<int>();

        for (int i = 0; i < enemyPosition.Length; i++) {
            Enemies.Add(i);
        }

        for (int i = 0; i < targetButtons.Length; i++) {
            if (enemyPosition[i].currentHP == 0) {
                targetButtons[i].gameObject.SetActive(false);
            } else {

                targetButtons[i].moveName = moveName;
                targetButtons[i].target = Enemies[i];
                targetButtons[i].targetName.text = enemyPosition[i].charName;
            }

        }

    }

    public void CloseTargetMenu() {

        targetMenu.SetActive(false);

    }

    public void OpenListMenu() {

        ListAttackMenu.SetActive(true);
        ListAttack.instance.movesNames = GameManager.instance.playerStats[currentTurn].moveAvailable;
        ListAttack.instance.ShowMoves();

    }

    public void CloseListMenu() {

        ListAttackMenu.SetActive(false);

    }

}