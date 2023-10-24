using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIDataManager : MonoBehaviour
{
    public Canvas staticCanvas;
    public Text plrMoney;
    public UnityEngine.UI.Image weaponIMG;
    public UnityEngine.UI.Image BackGroundIMG;
    public Text playerInfomations;
    public Transform regradeButton;
    public Transform saveButton;
    public Transform sellButton;
    public Dictionary<uint, ItemTable> itemDictionary = new Dictionary<uint, ItemTable>();
    public ItemTable nowWeapon = new ItemTable();
    private static UIDataManager instance;
    public static UIDataManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        DontDestroyOnLoad(staticCanvas.gameObject);
        InGameUIOnOFF(false);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void InGameUIOnOFF(bool value)
    {
        plrMoney.gameObject.SetActive(value);
        weaponIMG.gameObject.SetActive(value);
        playerInfomations.gameObject.SetActive(value);
        regradeButton.gameObject.SetActive(value);
        saveButton.gameObject.SetActive(value);
        sellButton.gameObject.SetActive(value);
    }
    public void SetAccountValue(PlayerInventory tempInven)
    {
        changeNowWeapon(tempInven.WeaponIndex);
        playerInfomations.text = "유저 ID : " + GameManager.Instance.playerInven.userID + "\n"
            + "최대 강화기록 : " + GameManager.Instance.playerInven.maxRegrade + "\n" +
            "강화 비용 : " + nowWeapon.regradeValue + "\n" +
            "현재 무기 강화치 : " + GameManager.Instance.playerInven.WeaponIndex + "\n" +
            "현재 무기 이름 : " + nowWeapon.itemName + "\n" +
            "무기 판매가 : " + nowWeapon.sellValue + "\n" +
            "무기 강화 확률 : " + nowWeapon.regradePercent + "%";
        plrMoney.text = "소지금 : "+ tempInven.money.ToString();

    }
    public void changeNowWeapon(uint index)
    {
        
        nowWeapon.Index = itemDictionary[index].Index;
        nowWeapon.itemName = itemDictionary[index].itemName;
        nowWeapon.codeName = itemDictionary[index].codeName;
        nowWeapon.sellValue = itemDictionary[index].sellValue;
        nowWeapon.buyValue = itemDictionary[index].buyValue;
        nowWeapon.regradePercent = itemDictionary[index].regradePercent;
        nowWeapon.regradeValue = itemDictionary[index].regradeValue;
        GameManager.Instance.playerInven.WeaponIndex = nowWeapon.Index;
        if (nowWeapon.Index> GameManager.Instance.playerInven.maxRegrade)
        {
            GameManager.Instance.playerInven.maxRegrade = nowWeapon.Index;
        }
        playerInfomations.text = "유저 ID : " + GameManager.Instance.playerInven.userID + "\n"
            + "최대 강화기록 : " + GameManager.Instance.playerInven.maxRegrade + "\n" +
            "강화 비용 : " + nowWeapon.regradeValue + "\n" +
            "현재 무기 강화치 : " + GameManager.Instance.playerInven.WeaponIndex + "\n" +
            "현재 무기 이름 : " + nowWeapon.itemName + "\n" +
            "무기 판매가 : " + nowWeapon.sellValue + "\n"+
            "무기 강화 확률 : " + nowWeapon.regradePercent+"%";
        plrMoney.text = "소지금 : " + GameManager.Instance.playerInven.money.ToString();
        changeIMG(index, ref weaponIMG);
    }
    public void changeIMG(uint itemIndex,ref UnityEngine.UI.Image target)
    {
        target.sprite = Resources.Load<Sprite>("WeaponIMG/Sword" + nowWeapon.Index);

        
    }
        
}
