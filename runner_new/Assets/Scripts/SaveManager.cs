using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public gameManager GM;
    public MenuController MC;
    public playerContol PM;
    public ShopManager SM;
    public AudioManager AM;
    public PlayerShopManager PSM;
    string filePath;
    public static SaveManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        GM = FindObjectOfType<gameManager>();
        MC = FindObjectOfType<MenuController>();
        filePath = Application.persistentDataPath + "data.gamesave";
        LoadGame();
        SaveGame();
    }
    public void SaveGame() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();
        save.Sound = MC.Sound;
        save.Music = MC.Music;
        save.ScanLine = MC.Scanline;
        save.Stats = GM.stats;
        save.Coins = GM.coins;
        save.rew_buff = GM.rewind_buff_count;
        save.ActivePlayerSkinIndex = (int)PSM.ActiveSkin;
        save.ActiveSkinIndex = (int)SM.ActiveSkin;
        save.SaveBoughtItems(SM.Item);
        save.SavePlayerBoughtItems(PSM.Item);
        save.save_BGS_number(AM.BGTS_number);

        save.showADS = PM.showAD;

        bf.Serialize(fs, save);
        fs.Close();
    }
    public void LoadGame() {
        if (!File.Exists(filePath)) {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        MC.Sound = save.Sound;
        MC.Music = save.Music;
        MC.Scanline = save.ScanLine;
        GM.stats = save.Stats;
        GM.coins = save.Coins;
        PM.showAD = save.showADS;
        GM.rewind_buff_count = save.rew_buff;
        SM.ActiveSkin = (ShopItem.ItemType)save.ActiveSkinIndex;
        PSM.ActiveSkin = (PlayerShopItem.ItemType)save.ActivePlayerSkinIndex;
        for (int i = 0; i < save.BoughtItem.Count; i++)
        {
            SM.Item[i].isBought = save.BoughtItem[i];
        }
        for (int j = 0; j < save.BoughtPlayerItems.Count; j++)
        {
            PSM.Item[j].isBought = save.BoughtPlayerItems[j];
        }
        for (int z = 0; z < save.BGS_number.Count; z++)
        {
            AM.BGTS_number.Add(save.BGS_number[z]);
        }
        fs.Close();
        GM.ActivatePlayerSkins((int)PSM.ActiveSkin);
        GM.ActivateSkins((int)SM.ActiveSkin);
    }
}
[System.Serializable]
public class Save
{
    public int showADS;
    public bool Sound;
    public bool Music;
    public bool ScanLine;
    public int[] Stats;
    public int Coins;
    public int rew_buff;
    public int ActivePlayerSkinIndex;
    public int ActiveSkinIndex;
    public List<bool> BoughtPlayerItems = new List<bool>();
    public List<bool> BoughtItem = new List<bool>();
    public List<int> BGS_number = new List<int>();
    public void save_BGS_number(List<int> tracks)
    {
        foreach (var track in tracks)
        {
            BGS_number.Add(track);
        }
    }
    public void SaveBoughtItems(List<ShopItem> items) {
        foreach (var item in items) {
            BoughtItem.Add(item.isBought);
        }
    }
    public void SavePlayerBoughtItems(List<PlayerShopItem> PlItems)
    {
        foreach (var pl_item in PlItems)
        {
            BoughtPlayerItems.Add(pl_item.isBought);
        }
    }
}
