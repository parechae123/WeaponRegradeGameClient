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
        playerInfomations.text = "���� ID : " + tempInven.userID+"\n"
            +"�ִ� ��ȭ��� : "+tempInven.maxRegrade+ "\n"+
            "���� ���� ��ȭġ : "+ tempInven.WeaponIndex;
        plrMoney.text = "������ : "+ tempInven.money.ToString();
        changeIMG(tempInven.WeaponIndex,ref weaponIMG);
    }
    public void GetItemDatas()
    {

    }
    public void changeIMG(uint itemIndex,ref UnityEngine.UI.Image target)
    {
        target.sprite = Resources.Load<Sprite>("WeaponIMG/Sword" + itemIndex);


    }
}
