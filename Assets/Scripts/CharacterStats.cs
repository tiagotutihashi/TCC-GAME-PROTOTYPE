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

    public EquipItem weapon;
    public EquipItem armor;

    public Sprite charImage;

    public string[] moveAvailable;

}
