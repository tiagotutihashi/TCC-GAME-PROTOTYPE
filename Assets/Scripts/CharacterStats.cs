using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public string charName;
    public int level;
    public int currentExp;
    public int maxLevel = 100;
    public int[] exptToLevelUp;
    public int baseExp = 30;

    public int baseHP;
    public int baseEne;
    public int baseAtt;
    public int baseDef;

    public int maxHP;
    public int currentHP;
    public int maxEne;
    public int currentEne;

    public int attack;
    public int defense;
    public int spAttack;
    public int spDefense;

    /*public string wpnName;
    public int wpnPwr;
    public string armName;
    public int armPwr;*/

    public EquipItem weapon;
    public EquipItem armor;

    public Sprite charImage;

    void Start() {

        exptToLevelUp = new int[maxLevel];

        exptToLevelUp[1] = baseExp;

        for (int i = 2; i < exptToLevelUp.Length; i++) {

            exptToLevelUp[i] = Mathf.FloorToInt(exptToLevelUp[i-1] * 1.10f);

        }

    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.K)) {
            AddExp(30);
        }

    }

    public void AddExp(int exp) {

        TerminalManager.instance.ShowInTerminal("CharacterStats.AddExp("+ exp +")");

        currentExp += exp;

        if (level < maxLevel) {
            if (currentExp >= exptToLevelUp[level]) {
                LevelUp();
            }
        }  

        if(level >= maxLevel){
            currentExp = 0;
        }

    }

    private void LevelUp() {

        TerminalManager.instance.ShowInTerminal("CharacterStats.LevelUp()");

        currentExp -= exptToLevelUp[level];
        level++;

        maxHP = Mathf.FloorToInt(level * 3.8f) + baseHP;
        maxEne = Mathf.FloorToInt(level * 1.2f) + baseHP;

        attack = Mathf.FloorToInt(level * 1.8f) + baseAtt;
        defense = Mathf.FloorToInt(level * 2.8f) + baseDef;

    }

}
