using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour {

    public static EquipManager instance;

    public List<UseEquipItem> btnEquip;
    public UseEquipItem refe;
    public Button open;

    public bool first = false;

    void Start() {

        instance = this;

    }

    public void LoadEquip() {

        TerminalManager.instance.ShowInTerminal("EquipManager.LoadEquip()");

        DestroyItems();

        EquipItem[] loaded = GameManager.instance.GetPlayerEquipItems().ToArray();

        for (int i = 0; i < loaded.Length; i++) {

            UseEquipItem newB = Instantiate(refe, gameObject.transform);
            newB.equipToUse = loaded[i];
            Button b = newB.transform.Find("Equip Details").GetComponent<Button>();
            b.gameObject.transform.Find("Image").GetComponent<Image>().sprite = loaded[i].image;
            b.gameObject.transform.Find("Item Name").GetComponent<Text>().text = loaded[i].itemName;
            b.gameObject.transform.Find("Description").GetComponent<Text>().text = loaded[i].description;

            if(loaded[i].att > 0) {
                b.gameObject.transform.Find("Stats").GetComponent<Text>().text = "Att: ";
                b.gameObject.transform.Find("Value").GetComponent<Text>().text = loaded[i].att.ToString();
            } else {
                b.gameObject.transform.Find("Stats").GetComponent<Text>().text = "Def: ";
                b.gameObject.transform.Find("Value").GetComponent<Text>().text = loaded[i].def.ToString();
            }

            for(int f = 0; f < GameManager.instance.playerStats.Length; f++) {

                if(GameManager.instance.playerStats[f].weapon == loaded[i] || GameManager.instance.playerStats[f].armor == loaded[i]) {
                    b.gameObject.transform.Find("Equip").GetComponent<Text>().text = GameManager.instance.playerStats[f].charName;
                }

            }
          
            btnEquip.Add(newB);

        }

        first = true;

    }

    public void LoadItemsButton() {

        TerminalManager.instance.ShowInTerminal("EquipManager.LoadItemsButton()");

        if (open.gameObject.activeInHierarchy && !first) {
            LoadEquip();
            first = true;
        }

    }

    public void DestroyItems() {

        TerminalManager.instance.ShowInTerminal("EquipManager.DestroyItems()");

        int childs = transform.childCount;
        for (int i = 0; i < childs; i++) {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        first = false;

    }

    public void AddItem(EquipItem addItem) {

        TerminalManager.instance.ShowInTerminal("EquipManager.AddItem(" + addItem.itemName + ")");

        List<EquipItem> loaded = GameManager.instance.GetPlayerEquipItems();

        bool have = false;

        for (int i = 0; i < loaded.Count; i++) {

            if (loaded[i].itemName == addItem.itemName) {
                have = true;
                break;
            }

        }

        if (!have) {
            loaded.Add(addItem);
            GameManager.instance.haveEquipItems.Add(addItem.itemName);
        }

    }

}
