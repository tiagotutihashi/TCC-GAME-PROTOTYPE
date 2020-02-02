using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public CharacterStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    [Header("Use Items")]
    public UseItem[] useItems;
    private List<UseItem> playerUseItems = new List<UseItem>();
    public List<string> haveUseItems;
    public List<int> amountHaveUseItems;

    [Header("Weapon Items")]
    public EquipItem[] equipItems;
    public List<string> haveEquipItems;
    public List<EquipItem> playerEquipItems = new List<EquipItem>();

    void Start() {

        instance = this;

        DontDestroyOnLoad(gameObject);

    }
    
    void Update() {
        
        if(gameMenuOpen || dialogActive || fadingBetweenAreas) {
            PlayerController.instance.canMove = false;
        } else {
            PlayerController.instance.canMove = true;
        }

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

}
