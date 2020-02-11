using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        FIRST_SKIN, SECOND_SKIN, THIRD_SKIN
    }
    public TextMeshProUGUI cost_txt, status_txt;
    public ItemType Type;
    public Button buyBtn,applBtn;
    public bool isBought;
    public int Cost;
    gameManager GM;
    public ShopManager SM;
    int format_for_cost = 10000;
    int result_cost = 0;
    string st_result_cost;
    public string cost_format = "00:00";
    bool isActive{
        get {
            return Type == SM.ActiveSkin;
        }
    }
    public void Init() {
        GM = FindObjectOfType<gameManager>();
        cost_formater();
        cost_txt.text = cost_format;
    }
    void cost_formater()
    {
        result_cost = format_for_cost + (int)Cost;
        st_result_cost = result_cost.ToString();
        cost_format = string.Format("{0}{1}:{2}{3}", st_result_cost[1], st_result_cost[2], st_result_cost[3], st_result_cost[4]);
        if (isBought)
        {
            status_txt.text = "owned";
            cost_format = "--:--";
        }
        else
        {
            status_txt.text = "not owned";
        }
    }
    public void CheckBtn() {
        buyBtn.gameObject.SetActive(!isBought);
        buyBtn.interactable = canBuy();
        applBtn.gameObject.SetActive(isBought);
        applBtn.interactable = !isActive;
    }
    bool canBuy() {
        return GM.coins >= Cost;
    }
    public void BuyItemm() {
        if (!canBuy()) {
            return;
        }
        isBought = true;
        GM.coins -= Cost;
        CheckBtn();
        SM.checkItemButtons();
        //refreshcoins
        SaveManager.Instance.SaveGame();
    }
    public void ActivateItem() {
        SM.ActiveSkin = Type;
        SM.checkItemButtons();
        switch (Type) {
            case ItemType.FIRST_SKIN:
                GM.ActivateSkins(0);
                break;
            case ItemType.SECOND_SKIN:
                GM.ActivateSkins(1);
                break;
            case ItemType.THIRD_SKIN:
                GM.ActivateSkins(2);
                break;
        }
        SaveManager.Instance.SaveGame();
    }
}
