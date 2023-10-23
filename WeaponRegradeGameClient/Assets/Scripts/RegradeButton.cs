using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegradeButton : MonoBehaviour
{
    public void OnClickRegradeBTN()
    {
        if (isRegradeSucces()&& GameManager.Instance.playerInven.money - UIDataManager.Instance.nowWeapon.regradeValue > 0)
        {
            UIDataManager.Instance.changeNowWeapon(UIDataManager.Instance.nowWeapon.Index+1);
            
        }
        else if(isRegradeSucces() && GameManager.Instance.playerInven.money - UIDataManager.Instance.nowWeapon.regradeValue > 0)
        {
            UIDataManager.Instance.changeNowWeapon(1);
        }
        GameManager.Instance.playerInven.money -= UIDataManager.Instance.nowWeapon.regradeValue;
    }
    private bool isRegradeSucces()
    {

        if (Random.Range((int)0,(int)101)>=100-(UIDataManager.Instance.nowWeapon.regradePercent))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}