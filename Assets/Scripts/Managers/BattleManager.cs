using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    public static BattleManager instace;

    public bool battleActive;

    public GameObject battleScene;

    public GameObject[] playerPosition;
    public EnemyStats[] enemyPosition;

    public EnemyStats[] enemies;

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
    public ListAttack listAttacks;

    public GameObject painelMessage;

    public GameObject itemMenu;
    public ItemList itemList;
    public BattleItem[] itemButtons;

    public GameObject playerTargetMenu;
    public PlayerTarget[] playerTargets;

    public GameObject[] statusPanel;
    public GameObject endPanel;
    public GameObject winPanel;

    public Text[] playersNames;
    public Slider[] playerExpSlider;
    public Text[] playerExpText;
    public Text[] playerLevelUpText;
    public Text goldText;

    public bool showStatus = false;
    public bool espaceFail = false;
    private bool loseTheBattle = false;
    private bool winTheBattle = false;
    private bool goToMainMenu = false;
    private bool updateExp = true;
    private bool showOnce = true;
    public string nameEnemy;

    void Start() {

        instace = this;
        DontDestroyOnLoad(gameObject);

        TerminalManager.instance.ShowInTerminalObject("BattleManger - BattleManager");

    }

    void Update() {

        if (battleActive) {
            if (loseTheBattle) {
                if(showOnce)
                    StartCoroutine(EndBattle());
                if (goToMainMenu) {
                    StartCoroutine(GoToMainMenu());
                }
            } else if (winTheBattle) {
                if (updateExp)
                    UpdateExp();
                if(showOnce)
                    StartCoroutine(WinBattle());
            } else {
                if (turnWaiting) {
                    if (currentTurn > 1) {
                        //Enemy Attack
                        PlayerMenu.SetActive(false);
                        StartCoroutine(EnemyMoveCo());
                    } else {
                        if (espaceFail) {
                            PlayerMenu.SetActive(false);
                            StartCoroutine(FailMessage());
                        } else {
                            PlayerMenu.SetActive(true);
                        }
                    }
                }
            }

        }


    }

    public void UpdateStatus() {

        TerminalManager.instance.ShowInTerminal("BattleManager.UpdateStatus()");

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {

            PlayerStats playChar = GameManager.instance.playerStats[i];

            playName[i].text = playChar.charName;
            playLevel[i].text = playChar.level.ToString();
            playhp[i].text = playChar.currentHP.ToString() + "/" + playChar.maxHP.ToString();
            playEne[i].text = playChar.currentEne.ToString() + "/" + playChar.maxEne.ToString();
            playSliderHp[i].maxValue = playChar.maxHP;
            playSliderHp[i].value = playChar.currentHP;
            playSliderEne[i].maxValue = playChar.maxEne;
            playSliderEne[i].value = playChar.currentEne;

        }

        if (showStatus) {

            for (int i = 0; i < enemyPosition.Length; i++) {

                EnemyStats playChar = enemyPosition[i];

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

    }

    public void BattleStart(string[] enemeisToBattle, string nameEnemy) {

        TerminalManager.instance.ShowInTerminal("BattleManager.BattleStart([" + enemeisToBattle[0] + ", " + enemeisToBattle[1] + "])");

        GameMenu.instance.configButtons.SetActive(false);
        
        this.nameEnemy = nameEnemy;

        endPanel.SetActive(false);
        winPanel.SetActive(false);
        showStatus = false;
        espaceFail = false;
        loseTheBattle = false;
        winTheBattle = false;
        goToMainMenu = false;
        updateExp = true;
        currentTurn = 0;
        showOnce = true;

        for (int i = 0; i < statusPanel.Length; i++) {
            statusPanel[i].SetActive(true);
        }

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
                        enemyPosition[i].currentExp = enemies[f].currentExp;
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
            UpdateStatus();

        }

    }

    public void NextTurn() {

        TerminalManager.instance.ShowInTerminal("BattleManager.NextTurn()");

        currentTurn++;
        if (currentTurn > 3) {
            currentTurn = 0;
        }
        turnWaiting = true;
    }

    public void UpdateBattle() {

        TerminalManager.instance.ShowInTerminal("BattleManager.UpdateBattle()");

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
                winTheBattle = true;
            } else {
                loseTheBattle = true;
            }

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

    public IEnumerator WinBattle() {

        showOnce = false;
        TerminalManager.instance.ShowInTerminal("BattleManager.WinBattle()");

        nameEnemy = "";
        PlayerMenu.SetActive(false);
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        winPanel.SetActive(true);

    }

    public void UpdateExp() {

        TerminalManager.instance.ShowInTerminal("BattleManager.UpdateExp()");

        int[] levelUp = new int[] { 0, 0 };

        for (int i = 0; i < statusPanel.Length; i++) {
            statusPanel[i].SetActive(false);
        }
        int expToGain = 0;
        int goldToGain = 0;
        for (int i = 0; i < enemyPosition.Length; i++) {
            expToGain += enemyPosition[i].expToGive;
            goldToGain += enemyPosition[i].goldToGive;
        }

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {
            levelUp[i] = GameManager.instance.playerStats[i].AddExp(expToGain);
        }

        GameManager.instance.money += goldToGain;

        for (int i = 0; i < playersNames.Length; i++) {
            playersNames[i].text = GameManager.instance.playerStats[i].charName;
            playerExpText[i].text = "Exp: " + GameManager.instance.playerStats[i].currentExp + "/" + GameManager.instance.playerStats[i].exptToLevelUp[GameManager.instance.playerStats[i].level];
            playerExpSlider[i].maxValue = GameManager.instance.playerStats[i].exptToLevelUp[GameManager.instance.playerStats[i].level];
            playerExpSlider[i].value = GameManager.instance.playerStats[i].currentExp;
            goldText.text = "Ouro: " + goldToGain;
            if (levelUp[i] > 0)
                playerLevelUpText[i].text = "Subiu de nível: +" + levelUp[i].ToString();
        }

        updateExp = false;

    }

    public void ContinueTheGame() {

        TerminalManager.instance.ShowInTerminal("BattleManager.ContinueTheGame()");

        winPanel.SetActive(false);
        winTheBattle = false;
        UIFade.instance.FadeFromBlack();
        battleActive = false;
        GameManager.instance.battleActive = false;
        battleScene.SetActive(false);
        playerLevelUpText[0].text = "";
        playerLevelUpText[1].text = "";
        GameMenu.instance.configButtons.SetActive(true);

    }

    public IEnumerator EndBattle() {

        showOnce = false;
        TerminalManager.instance.ShowInTerminal("BattleManager.EndBattle()");

        for (int i = 0; i < statusPanel.Length; i++) {
            statusPanel[i].SetActive(false);
        }
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        endPanel.SetActive(true);
        
    }

    public void Continue() {

        TerminalManager.instance.ShowInTerminal("BattleManager.Continue()");

        UIFade.instance.FadeFromBlack();
        endPanel.SetActive(false);
        battleScene.SetActive(false);
        GameManager.instance.battleActive = false;
        battleActive = false;
        loseTheBattle = false;
        for (int i = 0; i < statusPanel.Length; i++) {
            statusPanel[i].SetActive(true);
        }
        GameManager.instance.LoadGame();

    }

    public void MainMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.MainMenu()");

        goToMainMenu = true;

    }

    public void ExitGame() {

        TerminalManager.instance.ShowInTerminal("BattleManager.ExitGame()");

        Application.Quit();

    }

    public IEnumerator GoToMainMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.GoToMainMenu()");

        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(.1f);
        /*SceneManager.LoadScene("MainMenu");
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(gameObject);
        Destroy(UIFade.instance.gameObject);
        battleScene.SetActive(false);
        GameManager.instance.battleActive = false;
        battleActive = false;*/
        GameManager.instance.DestroyAllnGoToMainMenu();

    }

    public IEnumerator EnemyMoveCo() {

        TerminalManager.instance.ShowInTerminal("BattleManager.EnemyMoveCo()");

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

        TerminalManager.instance.ShowInTerminal("BattleManager.DealDamage(" + target + ", " + movePower + ")");

        float attPwr = enemyPosition[currentTurn - 2].attack;
        float defPwr = GameManager.instance.playerStats[target].defense;

        float damage = (movePower * attPwr) / defPwr;

        int damageToGive = Mathf.RoundToInt(damage);

        GameManager.instance.playerStats[target].currentHP -= damageToGive;

        UpdateBattle();

    }

    public void DealDamageEnemy(int target, int movePower) {

        TerminalManager.instance.ShowInTerminal("BattleManager.DealDamageEnemy(" + target + ", " + movePower + ")");

        float attPwr = GameManager.instance.playerStats[currentTurn].attack;
        float defPwr = enemyPosition[target].defense;

        float damage = (movePower * attPwr) / defPwr;

        int damageToGive = Mathf.RoundToInt(damage);

        enemyPosition[target].currentHP -= damageToGive;

        UpdateBattle();

    }

    public IEnumerator PlayerAttack(string moveName, int selectTarget) {

        TerminalManager.instance.ShowInTerminal("BattleManager.PlayerAttack(" + moveName + ", " + selectTarget + ")");

        int movePower = 0;
        int moveCost = 0;
        PlayerMenu.SetActive(false);
        for (int i = 0; i < movesList.Length; i++) {
            if (movesList[i].moveName == moveName) {
                Instantiate(movesList[i].theEffect, enemyPosition[selectTarget].transform.position, enemyPosition[selectTarget].transform.rotation);
                movePower = movesList[i].movePower;
                moveCost = movesList[i].moveCost;
                yield return new WaitForSeconds(movesList[i].theEffect.effectLenght);
            }
        }

        GameManager.instance.playerStats[currentTurn].currentEne -= moveCost;
        DealDamageEnemy(selectTarget, movePower);
        NextTurn();
        targetMenu.SetActive(false);
        ListAttackMenu.SetActive(false);
        listAttacks.disableList();

    }

    public void OpenTargetMenu(string moveName) {

        TerminalManager.instance.ShowInTerminal("BattleManager.OpenTargetMenu(" + moveName + ")");

        targetMenu.SetActive(true);

        List<int> Enemies = new List<int>();

        for (int i = 0; i < enemyPosition.Length; i++) {
            Enemies.Add(i);
        }

        for (int i = 0; i < targetButtons.Length; i++) {
            if (enemyPosition[i].currentHP == 0) {
                targetButtons[i].gameObject.SetActive(false);
            } else {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].target = Enemies[i];
                targetButtons[i].targetName.text = enemyPosition[i].charName;
            }

        }

    }

    public void CloseTargetMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.CloseTargetMenu()");

        targetMenu.SetActive(false);

    }

    public void OpenListMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.OpenListMenu()");

        ListAttackMenu.SetActive(true);
        listAttacks.movesNames = GameManager.instance.playerStats[currentTurn].moveAvailable;
        listAttacks.energy = GameManager.instance.playerStats[currentTurn].currentEne;
        listAttacks.ShowMoves();
        listAttacks.CreateBack();

    }

    public void CloseListMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.CloseListMenu()");

        ListAttackMenu.SetActive(false);
        targetMenu.SetActive(false);

    }

    public void Escape() {

        TerminalManager.instance.ShowInTerminal("BattleManager.Escape()");

        PlayerMenu.SetActive(false);

        int levelD = 0;
        for (int i = 0; i < enemyPosition.Length; i++) {
            for (int f = 0; f < GameManager.instance.playerStats.Length; f++) {
                levelD += GameManager.instance.playerStats[f].level - enemyPosition[i].level;
            }
        }
        int attempt = Random.Range(0, 100);

        if (attempt < 20 + levelD * 5) {

            battleActive = false;
            battleScene.SetActive(false);

        } else {
            espaceFail = true;

        }

    }

    public IEnumerator FailMessage() {

        TerminalManager.instance.ShowInTerminal("BattleManager.FailMessage()");

        NextTurn();
        UpdateBattle();
        PlayerMenu.SetActive(false);
        painelMessage.SetActive(true);
        painelMessage.GetComponentInChildren<Text>().text = "Não escapou";
        espaceFail = false;
        yield return new WaitForSeconds(2f);
        painelMessage.SetActive(false);
        PlayerMenu.SetActive(false);

    }

    public void Inspect() {

        TerminalManager.instance.ShowInTerminal("BattleManager.Inspect()");

        showStatus = true;
        NextTurn();
        UpdateBattle();

    }

    public void OpenPlayerTargetMenu(UseItem item) {

        TerminalManager.instance.ShowInTerminal("BattleManager.OpenPlayerTargetMenu(" + item + ")");

        playerTargetMenu.SetActive(true);

        List<int> Players = new List<int>();

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++) {
            Players.Add(i);
        }

        for (int i = 0; i < playerTargets.Length; i++) {
            playerTargets[i].GetComponent<Button>().interactable = true;
            playerTargets[i].item = item;
            playerTargets[i].target = Players[i];
            playerTargets[i].targetName.text = GameManager.instance.playerStats[i].charName;

            if ((item.hp > 0 && GameManager.instance.playerStats[i].currentHP == GameManager.instance.playerStats[i].maxHP)) {
                playerTargets[i].GetComponent<Button>().interactable = false;
            }
            if ((item.ene > 0 && GameManager.instance.playerStats[i].currentEne == GameManager.instance.playerStats[i].maxEne)) {
                playerTargets[i].GetComponent<Button>().interactable = false;
            }

        }

    }
    public void ClosePlayerTargetMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.ClosePlayerTargetMenu()");

        playerTargetMenu.SetActive(false);

    }

    public void OpenItemMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.OpenItemMenu()");

        itemMenu.SetActive(true);
        itemList.ShowMoves();
        itemList.CreateBack();

    }

    public void CloseItemMenu() {

        TerminalManager.instance.ShowInTerminal("BattleManager.CloseItemMenu()");

        itemMenu.SetActive(false);
        targetMenu.SetActive(false);

    }

    public IEnumerator PlayerUseItem(UseItem item, int selectTarget) {

        TerminalManager.instance.ShowInTerminal("BattleManager.PlayerUseItem(" + item + ", " + selectTarget + ")");

        PlayerMenu.SetActive(false);

        PlayerStats charToUse = GameManager.instance.playerStats[selectTarget];

        if (item.hp + charToUse.currentHP >= charToUse.maxHP) {
            charToUse.currentHP = charToUse.maxHP;
        } else {
            charToUse.currentHP += item.hp;
        }

        if (item.ene + charToUse.currentEne >= charToUse.maxEne) {
            charToUse.currentEne = charToUse.maxEne;
        } else {
            charToUse.currentEne += item.ene;
        }

        List<UseItem> loaded = GameManager.instance.GetPlayerUseItems();

        for (int i = 0; i < loaded.Count; i++) {

            if (loaded[i].itemName == item.itemName) {
                GameManager.instance.amountHaveUseItems[i]--;
                if (GameManager.instance.amountHaveUseItems[i] <= 0) {
                    loaded.Remove(item);
                    GameManager.instance.amountHaveUseItems.RemoveAt(i);
                    GameManager.instance.haveUseItems.RemoveAt(i);
                }
                break;
            }

        }

        itemList.disableList();

        NextTurn();
        UpdateStatus();

        playerTargetMenu.SetActive(false);
        itemMenu.SetActive(false);
        itemList.disableList();

        yield return new WaitForSeconds(0f);

    }

}