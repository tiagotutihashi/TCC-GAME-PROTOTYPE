using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {
    void Start() {

        exptToLevelUp = new int[maxLevel];

        exptToLevelUp[1] = baseExp;

        for (int i = 2; i < exptToLevelUp.Length; i++) {

            exptToLevelUp[i] = Mathf.FloorToInt(exptToLevelUp[i - 1] * 1.10f);

        }

    }

    public int AddExp(int exp) {

        int levelUp = 0;

        TerminalManager.instance.ShowInTerminal("PlayerStats.AddExp(" + exp + ")");

        currentExp += exp;

        while (currentExp >= exptToLevelUp[level]) {
            if (level < maxLevel) {
                if (currentExp >= exptToLevelUp[level]) {
                    LevelUp();
                    levelUp++;
                }
            }
        }

        if (level >= maxLevel) {
            currentExp = 0;
        }

        return levelUp;

    }

    private void LevelUp() {

        TerminalManager.instance.ShowInTerminal("PlayerStats.LevelUp()");

        currentExp -= exptToLevelUp[level];
        level++;

        maxHP = Mathf.FloorToInt(level * 3.8f) + baseHP;
        maxEne = Mathf.FloorToInt(level * 1.2f) + baseHP;

        attack = Mathf.FloorToInt(level * 1.8f) + baseAtt;
        defense = Mathf.FloorToInt(level * 2.8f) + baseDef;

    }

}
