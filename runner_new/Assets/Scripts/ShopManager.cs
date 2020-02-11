using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject item1, item2, item3;
    public GameObject previewModel1, previewModel2, previewModel3;
    public TextMeshProUGUI Balance;
    int ItemNumber = 1;
    public List<ShopItem> Item;
    public ShopItem.ItemType ActiveSkin;
    public gameManager GM;
    public void OpenShop() {
        checkItemButtons();
    }
    public void checkItemButtons() {
        foreach (ShopItem item in Item) {
            item.SM = this;
            item.Init();
            item.CheckBtn();
        }
        GM.coinsFormatter(GM.coins);
        Balance.text = GM.coins_format;
    }
    public void nextOne() {
        if (ItemNumber < 3)
        {
            ItemNumber++;
        }
        else
        {
            ItemNumber = 1;
        }
        ActiveItem();
        checkItemButtons();
    }
    public void previousOne() {
        if (ItemNumber > 1)
        {
            ItemNumber--;
        }
        else
        {
            ItemNumber = 3;
        }
        ActiveItem();
        checkItemButtons();
    }
    public void ActiveItem() {
        switch (ItemNumber) {
            case 1:
                item1.SetActive(true);
                previewModel1.SetActive(true);
                item2.SetActive(false);
                previewModel2.SetActive(false);
                item3.SetActive(false);
                previewModel3.SetActive(false);
                break;
            case 2:
                item1.SetActive(false);
                previewModel1.SetActive(false);
                item2.SetActive(true);
                previewModel2.SetActive(true);
                item3.SetActive(false);
                previewModel3.SetActive(false);
                break;
            case 3:
                item1.SetActive(false);
                previewModel1.SetActive(false);
                item2.SetActive(false);
                previewModel2.SetActive(false);
                item3.SetActive(true);
                previewModel3.SetActive(true);
                break;
        }
    }
}
