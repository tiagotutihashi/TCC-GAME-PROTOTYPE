using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public PlayerStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;
    public bool battleActive;

    [Header("Use Items")]
    public UseItem[] useItems;
    private List<UseItem> playerUseItems = new List<UseItem>();
    public List<string> haveUseItems;
    public List<int> amountHaveUseItems;

    [Header("Weapon Items")]
    public EquipItem[] equipItems;
    public List<string> haveEquipItems;
    public List<EquipItem> playerEquipItems = new List<EquipItem>();

    public int money;
    public bool canMove = true;

    void Start() {

        instance = this;

        DontDestroyOnLoad(gameObject);

        TerminalManager.instance.ShowInTerminalObject("GameManager - GameManager");

    }
    
    void Update() {
        
        if(gameMenuOpen || dialogActive || fadingBetweenAreas  || battleActive) {
            canMove = false;
        } else {
            canMove = true;
        }

        PlayerController.instance.canMove = canMove;

        if (Input.GetKeyDown(KeyCode.B)) {
            SaveGame();
        } 
        if (Input.GetKeyDown(KeyCode.V)) {
            LoadGame();
        }

    }

    public void DestroyAllnGoToMainMenu() {

        UIFade.instance.FadeToBlack();
        Destroy(BattleManager.instace.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(UIFade.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);

    }

    private void LoadItems() {

        TerminalManager.instance.ShowInTerminal("GameManager.LoadItems()");

        playerUseItems.Clear();

        for(int i = 0; i < haveUseItems.Count; i++) {

            for(int f = 0; f < useItems.Length; f++) {

                if(haveUseItems[i] == useItems[f].itemName) {

                    playerUseItems.Add(useItems[f]);

                }

            }

        }

    }

    private void LoadEquip() {

        TerminalManager.instance.ShowInTerminal("GameManager.LoadEquip()");

        playerEquipItems.Clear();

        for (int i = 0; i < haveEquipItems.Count; i++) {

            for (int f = 0; f < equipItems.Length; f++) {

                if (haveEquipItems[i] == equipItems[f].itemName) {

                    playerEquipItems.Add(equipItems[f]);

                }

            }

        }

    }

    public List<UseItem> GetPlayerUseItems() {

        LoadItems();
        return playerUseItems;

    }

    public List<EquipItem> GetPlayerEquipItems() {

        LoadEquip();
        return playerEquipItems;

    }

    public void SaveGame() {

        TerminalManager.instance.ShowInTerminal("GameManager.SaveGame()");

        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        for(int i = 0; i < playerStats.Length; i++) {

            PlayerPrefs.SetString("Player_" + i, playerStats[i].charName);
            PlayerPrefs.SetInt("Player_" + i + "_Level", playerStats[i].level);
            PlayerPrefs.SetInt("Player_" + i + "_Current_EXP", playerStats[i].currentExp);
            PlayerPrefs.SetInt("Player_" + i + "_Max_HP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + i + "_Current_HP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + i + "_Max_ENE", playerStats[i].maxEne);
            PlayerPrefs.SetInt("Player_" + i + "_Current_ENE", playerStats[i].currentEne);
            PlayerPrefs.SetInt("Player_" + i + "_Attack", playerStats[i].attack);
            PlayerPrefs.SetInt("Player_" + i + "_SP_Attack", playerStats[i].spAttack);
            PlayerPrefs.SetInt("Player_" + i + "_Defense", playerStats[i].defense);
            PlayerPrefs.SetInt("Player_" + i + "_SP_Defense", playerStats[i].spDefense);
            PlayerPrefs.SetString("Player_" + i + "_Weapon", playerStats[i].weapon.itemName);
            PlayerPrefs.SetString("Player_" + i + "_Armor", playerStats[i].armor.itemName);

        }

        PlayerPrefs.SetInt("Player_EquipCount", haveEquipItems.Count);

        for(int i = 0; i < haveEquipItems.Count; i++) {
            PlayerPrefs.SetString("Player_Equip_" + i, haveEquipItems[i]);
        }

        PlayerPrefs.SetInt("Player_UseItemCount", haveUseItems.Count);

        for (int i = 0; i < haveUseItems.Count; i++) {
            PlayerPrefs.SetString("Player_UseItem_" + i, haveUseItems[i]);
            PlayerPrefs.SetInt("Player_UseItem_Amount_" + i, amountHaveUseItems[i]);
        }

        PlayerPrefs.SetInt("Player_Money", money);

    }

    public void LoadGame() {

        TerminalManager.instance.ShowInTerminal("GameManager.LoadGame()");

        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        haveEquipItems.Clear();

        for (int i = 0; i < PlayerPrefs.GetInt("Player_EquipCount"); i++) {

            haveEquipItems.Add(PlayerPrefs.GetString("Player_Equip_" + i));

        }

        haveUseItems.Clear();
        amountHaveUseItems.Clear();

        for (int i = 0; i < PlayerPrefs.GetInt("Player_UseItemCount"); i++) {

            haveUseItems.Add(PlayerPrefs.GetString("Player_UseItem_" + i));
            amountHaveUseItems.Add(PlayerPrefs.GetInt("Player_UseItem_Amount_" + i));

        }

        for (int i = 0; i < playerStats.Length; i++) {

            playerStats[i].charName = PlayerPrefs.GetString("Player_" + i);
            playerStats[i].level = PlayerPrefs.GetInt("Player_" + i + "_Level");
            playerStats[i].currentExp = PlayerPrefs.GetInt("Player_" + i + "_Current_EXP");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + i + "_Max_HP");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + i + "_Current_HP");
            playerStats[i].maxEne = PlayerPrefs.GetInt("Player_" + i + "_Max_ENE");
            playerStats[i].currentEne =  PlayerPrefs.GetInt("Player_" + i + "_Current_ENE");
            playerStats[i].attack = PlayerPrefs.GetInt("Player_" + i + "_Attack");
            playerStats[i].spAttack = PlayerPrefs.GetInt("Player_" + i + "_SP_Attack");
            playerStats[i].defense = PlayerPrefs.GetInt("Player_" + i + "_Defense");
            playerStats[i].spDefense = PlayerPrefs.GetInt("Player_" + i + "_SP_Defense");


            for (int f = 0; f < equipItems.Length; f++) {

                if(PlayerPrefs.GetString("Player_" + i + "_Weapon") == equipItems[f].itemName) {
                    playerStats[i].weapon = equipItems[f];
                } else if (PlayerPrefs.GetString("Player_" + i + "_Armor") == equipItems[f].itemName) {
                    playerStats[i].armor = equipItems[f];
                }

            }
        }

        money = PlayerPrefs.GetInt("Player_Money");

    }

}
