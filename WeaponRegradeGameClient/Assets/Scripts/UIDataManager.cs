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
    public ItemTableList itemTableOnClient;
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
    }
    public void SetAccountValue(PlayerInventory tempInven)
    {
        playerInfomations.text = "유저 ID : " + tempInven.userID+"\n"
            +"최대 강화기록 : "+tempInven.maxRegrade+ "\n"+
            "현재 무기 강화치 : "+ tempInven.WeaponIndex;
        plrMoney.text = "소지금 : "+ tempInven.money.ToString();
        changeIMG(tempInven.WeaponIndex,ref weaponIMG);
    }
    public void changeIMG(uint itemIndex,ref UnityEngine.UI.Image target)
    {
        target.sprite = Resources.Load<Sprite>("WeaponIMG/Sword" + itemIndex);


    }
}
