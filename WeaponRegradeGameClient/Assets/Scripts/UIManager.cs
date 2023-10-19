using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public Canvas staticCanvas;
    public Text plrMoney;
    public UnityEngine.UI.Image weaponIMG;
    public UnityEngine.UI.Image BackGroundIMG;
    public Text playerInfomations;
    private static UIManager instance;
    public static UIManager Instance
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
    public void SetAccountValue(string plrName,uint money,uint weaponIndex)
    {
        playerInfomations.text = plrName;
        plrMoney.text = "¼ÒÁö±Ý : "+money.ToString();
        changeIMG(weaponIndex,ref weaponIMG);
    }
    public void changeIMG(uint itemIndex,ref UnityEngine.UI.Image target)
    {
        target.sprite = Resources.Load<Sprite>("WeaponIMG/Sword"+itemIndex);
    }
}
