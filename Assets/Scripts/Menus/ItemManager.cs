using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

    public static ItemManager instance;

    public List<UseItemToUse> btnItems;
    public UseItemToUse refe;
    public Button open;

    public bool first = false;

    void Start() {

        instance = this;

        TerminalManager.instance.ShowInTerminalObject("ItemManager - ItemManager");

    }

    public void LoadItems() {

        TerminalManager.instance.ShowInTerminal("ItemManager.LoadItems()");

        DestroyItems();

        UseItem[] loaded = GameManager.instance.GetPlayerUseItems().ToArray();

        for (int i = 0; i < loaded.Length; i++) {

                UseItemToUse newB = Instantiate(refe, gameObject.transform);
                newB.SetItemToUse(loaded[i]);
                Button b = newB.transform.Find("Item Details").GetComponent<Button>();
                b.gameObject.transform.Find("Image").GetComponent<Image>().sprite = loaded[i].image;
                b.gameObject.transform.Find("Item Name").GetComponent<Text>().text = loaded[i].itemName;
                b.gameObject.transform.Find("Quant").GetComponent<Text>().text = GameManager.instance.amountHaveUseItems[i].ToString();
                b.gameObject.transform.Find("Description").GetComponent<Text>().text = loaded[i].description;
                btnItems.Add(newB);
           
        }

        first = true;

    }


    public void LoadItemsButton() {

        TerminalManager.instance.ShowInTerminal("ItemManager.LoadItemsButton()");

        if (open.gameObject.activeInHierarchy && !first) {
            LoadItems();
            first = true;
        }

    }

    public void DestroyItems() {

        TerminalManager.instance.ShowInTerminal("ItemManager.DestroyItems()");

        int childs = transform.childCount;
        for (int i = 0; i < childs; i++) {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        first = false;

    }

    public void AddItem(UseItem addItem) {

        TerminalManager.instance.ShowInTerminal("ItemManager.AddItem("+ addItem.itemName +")");

        List<UseItem> loaded = GameManager.instance.GetPlayerUseItems();

        bool have = false;

        for(int i = 0; i < loaded.Count; i++) {

            if(loaded[i].itemName == addItem.itemName) {
                GameManager.instance.amountHaveUseItems[i]++;
                have = true;
                break;
            }

        }

        if (!have) {
            loaded.Add(addItem);
            GameManager.instance.haveUseItems.Add(addItem.itemName);
        }

    }

    public void RemoveItem(UseItem removeItem) {

        TerminalManager.instance.ShowInTerminal("ItemManager.RemoveItem(" + removeItem.itemName + ")");

        List<UseItem> loaded = GameManager.instance.GetPlayerUseItems();

        for (int i = 0; i < loaded.Count; i++) {

            if (loaded[i].itemName == removeItem.itemName) {
                GameManager.instance.amountHaveUseItems[i]--;
                if(GameManager.instance.amountHaveUseItems[i] <= 0) {
                    loaded.Remove(removeItem);
                    GameManager.instance.amountHaveUseItems.RemoveAt(i);
                    GameManager.instance.haveUseItems.RemoveAt(i);
                }
                break;
            }

        }

    }

}
