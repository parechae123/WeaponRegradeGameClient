using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }
    public void OnClickSellBTN()
    {
        Debug.Log("now sellValue" + UIDataManager.Instance.nowWeapon.sellValue);
        if (UIDataManager.Instance.nowWeapon.Index != 1)
        {
            GameManager.Instance.playerInven.money += UIDataManager.Instance.nowWeapon.sellValue;
            UIDataManager.Instance.changeNowWeapon(1);
        }
    }
}
